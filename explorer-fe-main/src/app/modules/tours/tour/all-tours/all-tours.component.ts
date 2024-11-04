import { Component, OnInit } from '@angular/core';
import {Service} from "../../../services/service";
import {TourDto} from "../../tour.model";
import {CartDto} from "../../cart.model";
import {AuthService} from "../../../services/auth/services/auth.service";
import * as L from "leaflet";
import {TokenService} from "../../../services/auth/services/token.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-all-tours',
  templateUrl: './all-tours.component.html',
  styleUrls: ['./all-tours.component.css']
})
export class AllToursComponent implements OnInit {
  tours: TourDto[] = [];
  maps: L.Map[] | undefined;
  filteredTours: TourDto[] = [];
  AuthorLoggedIn: boolean = false;
  TouristLoggedIn: boolean = false;
  selectedStatus: string = '';
  id: string | null  = '';
  selectedTour: TourDto | null = null;
  constructor(private service: Service,
              private tokenService: TokenService,
              private auth: AuthService,
              private router: Router) {}

  ngOnInit(): void {
    this.id = this.auth.getId()
    this.isLoggedAuthor()
    this.isLoggedTourist()
    this.getTours();
    setTimeout(() => {
      this.filteredTours.forEach(tour => this.initializeMap(tour));
    }, 0);

  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.tours.forEach(tour => {
        this.initializeMap(tour);
      });
    }, 0);
  }

  filterToursByStatus() {
    switch (this.selectedStatus) {
      case "top":
        this.service.getAllToursByTopAuthors().subscribe(tours => {
          this.filteredTours = tours;
          setTimeout(() => {
            this.filteredTours.forEach(tour => this.initializeMap(tour));
          }, 0);

        });
        break;
      case "draft":
      case "archived":
      case "published":
        this.service.filterToursByStatus(this.selectedStatus).subscribe(tours => {
          this.filteredTours = tours;
          setTimeout(() => {
            this.filteredTours.forEach(tour => this.initializeMap(tour));
          }, 0);

        });
        break;
      default:
        this.getTours();
        break;
    }
  }

  isLoggedAuthor(): boolean {
    this.AuthorLoggedIn = this.tokenService.getRole() === '1';
    return this.AuthorLoggedIn;
  }

  isLoggedTourist(): boolean {
    this.TouristLoggedIn = this.tokenService.getRole() === '0';
    return this.TouristLoggedIn;
  }


  initializeMap(tour: any): void {
    console.log(`Initializing map for tour id: ${tour.id}`);

    // Default location for Novi Sad, Serbia
    const defaultLocation = [45.2671, 19.8335];

    // Check if keyPoints exist and have at least one point
    const keyPoints = tour.keyPoints && tour.keyPoints.length > 0
      ? tour.keyPoints
      : [{ latitude: defaultLocation[0], longitude: defaultLocation[1] }];

    const map = L.map(`map-${tour.id}`).setView([keyPoints[0].latitude, keyPoints[0].longitude], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 18
    }).addTo(map);

    // Create an array of coordinates for the polyline
    const latLngs = keyPoints.map((keyPoint: any) => [keyPoint.latitude, keyPoint.longitude]);

    // Add markers for each key point
    keyPoints.forEach((keyPoint: any) => {
      L.marker([keyPoint.latitude, keyPoint.longitude])
        .addTo(map)
        .bindPopup(`<strong>${keyPoint.order || ''}</strong><br>${keyPoint.description || ''}<br>
      ${keyPoint.image ? `<img src="${keyPoint.image}" alt="${keyPoint.name || 'Image'}" style="max-width: 100px; height: auto;">` : ''}`);
    });

    // Add a polyline to connect the key points
    if (keyPoints.length > 1) {
      L.polyline(latLngs, { color: 'blue' }).addTo(map);
    }
  }
  getDifficultyLabel(difficulty: string) {
    if(difficulty == '0') return 'EASY'
    if(difficulty == '1') return 'MEDIUM'
    return 'HARD'
  }



  previewTour(tour: TourDto): void {
    this.router.navigate(['/tour-details', tour.id]);
  }

  addToCart(tour: TourDto): void {
    if(tour.ticketCount<1){
      return alert('not enough tickets!')
    }
    // Prepare the CartDTO
    const cartDto: CartDto = {
      tourId : tour.id,
      price: tour.price,
      touristId: Number(this.id),
      cartItems : []
    };

    //ADD TO CART SAMO AKO VEC NISI
    this.service.addToCart(cartDto).subscribe(() => {
      alert(`Tour added to cart!Tour name: ${tour.name}`);
    });
  }

  getTours(): void {
    if (this.isLoggedAuthor()) {

      console.log(this.id)
      this.service.getToursByAuthorId(Number(this.id)).subscribe(tours => {

        tours.sort((a: TourDto, b : TourDto) => a.id - b.id);

        this.filteredTours = tours;
        this.tours = tours;
        setTimeout(() => {
          this.filteredTours.forEach(tour => this.initializeMap(tour));
        }, 0);

      });
    } else {

      console.log('logged tourist?'+ this.isLoggedTourist())
      this.service.getAllPublishedTours().subscribe(tours => {

        tours.sort((a: TourDto, b : TourDto) => a.id - b.id);

        this.filteredTours = tours;
        this.tours = tours;
        setTimeout(() => {
          this.filteredTours.forEach(tour => this.initializeMap(tour));
        }, 0);

      });
    }
  }

  archive(tour: TourDto) {
    console.log(tour.status);
    this.service.archiveTour(Number(tour.id)).subscribe(tours => {
      alert("successfully archived tour!")
      this.getTours()
    });
  }

  statusLabel(status: string) {
      if( status === '2') return 'ARCHIVED'
      if( status === '1') return 'PUBLISHED'
      return 'DRAFT';
  }

  publish(tour: TourDto) {
    this.service.publishTour(Number(tour.id)).subscribe({
      next: (response) => {
        alert("Successfully published tour");
        this.getTours();
      },
      error: (err) => {
        alert("Not enough key points");
        console.error('Publish error:', err);
      }
    });
  }

}
