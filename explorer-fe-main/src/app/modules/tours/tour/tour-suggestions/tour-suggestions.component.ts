import {AfterViewInit, Component, OnInit} from '@angular/core';
import {Service} from "../../../services/service";
import {TourDto} from "../../tour.model";
import {CartDto} from "../../cart.model";
import {AuthService} from "../../../services/auth/services/auth.service";
import * as L from "leaflet";
import {Interest} from "../../interest";
import {TokenService} from "../../../services/auth/services/token.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-tour-suggestions',
  templateUrl: './tour-suggestions.component.html',
  styleUrls: ['./tour-suggestions.component.css']
})
export class TourSuggestionsComponent implements OnInit, AfterViewInit {
  tours: TourDto[] = [];
  myinterests: Interest[] = [];
  allInterests: Array<"NATURE" | "ART" | "SPORT" | "SHOPPING" | "FOOD"> = ["NATURE", "ART", "SPORT", "SHOPPING", "FOOD"];

  availableInterests: string[] = [];
  id: string | null = '';
  AuthorLoggedIn: boolean = false;
  TouristLoggedIn: boolean = false;

  constructor(
    private service: Service,
    private auth: AuthService,
    private tokenService: TokenService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.auth.getId();
    this.isLoggedAuthor();
    this.isLoggedTourist();
    this.getTours();
    this.getUserInterests(this.id);
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.tours.forEach(tour => {
        this.initializeMap(tour);
        this.getTourInterests(tour.id);
      });
    }, 0);
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
        .bindPopup(`<strong>${keyPoint.order || ''}</strong><br>${keyPoint.description || ''}
      <br>${keyPoint.image ? `<img src="${keyPoint.image}" alt="${keyPoint.name || 'Image'}" style="max-width: 100px; height: auto;">` : ''}`);
    });

    // Add a polyline to connect the key points
    if (keyPoints.length > 1) {
      L.polyline(latLngs, { color: 'blue' }).addTo(map);
    }
  }

  selectedStatus: string = '';
  filterToursByStatus() {
    console.log('Selected Status:', this.selectedStatus);
    if(this.selectedStatus !=='3'){
      this.service.recommendToursByDifficulty(Number(this.id), Number(this.selectedStatus)).subscribe(tours => {
        this.tours = tours;
        setTimeout(() => {
          this.tours.forEach(tour => {
            this.initializeMap(tour);
            this.getTourInterests(tour.id);
          });
        }, 0);
      });
    }else{
      this.getTours()
    }

  }
  getTours(): void {
    this.service.recommendTours(Number(this.id)).subscribe(tours => {
      this.tours = tours;
      setTimeout(() => {
        this.tours.forEach(tour => {
          this.initializeMap(tour);
          this.getTourInterests(tour.id);
        });
      }, 0);
    });
  }


  previewTour(tour: TourDto): void {
    this.router.navigate(['/tour-details', tour.id]);
  }

  addToCart(tour: TourDto): void {
    if (tour.ticketCount < 1) {
      return alert('Not enough tickets!');
    }

    const cartDto: CartDto = {
      tourId: tour.id,
      price: tour.price,
      touristId: Number(this.id),
      cartItems: []
    };

    this.service.addToCart(cartDto).subscribe(() => {
      alert(`Tour added to cart! Tour name: ${tour.name}`);
    });
  }


  private getUserInterests(id: string | null) {
    this.service.getAllInterestsByTouristId(Number(id)).subscribe(interests => {
      this.myinterests = interests;
      this.FilterAllInterests();
    });
  }

  getDifficultyLabel(difficulty: string) {
    if(difficulty == '0') return 'EASY'
    if(difficulty == '1') return 'MEDIUM'
    return 'HARD'
  }
  getTourInterests(tourId: number): void {
    this.service.getAllInterestsByTourId(tourId).subscribe(interests => {
      // Find the tour and set its interests
      const tour = this.tours.find(t => t.id === tourId);
      if (tour) {
        tour.interests = interests;
        console.log("interest:" +  tour.interests)
      }
    });
  }

  getInterestLabel(interest: Interest){
    var name = interest.interestTypeName;
    if(name == 0) return 'NATURE'
    if(name == 1) return 'ART'
    if(name == 2) return 'SPORT'
    if(name == 3) return 'SHOPPING'
    return 'FOOD'
  }

  removeInterest(interest: Interest) {
    this.service.deleteUserInterest(Number(interest.id)).subscribe(interests => {
      this.getUserInterests(this.id);
      this.getTours();
    });
  }
  private FilterAllInterests() {

    const myInterestLabels = this.myinterests.map(interest => this.getInterestLabel(interest));
    this.availableInterests = this.allInterests.filter(label => {
      return !myInterestLabels.includes(label);
    });
  }


  addInterest(interest: string) {
    var i = new Interest(4, Number(this.id), 0,0);
    if (interest == "NATURE"){
      i.interestTypeName = 0;
    }
    if (interest == "ART"){
      i.interestTypeName = 1;
    }
    if (interest == "SPORT"){
      i.interestTypeName = 2;
    }
    if (interest == "SHOPPING"){
      i.interestTypeName = 3;
    }
    this.service.createUserInterest(i).subscribe(interests => {
      this.getUserInterests(this.id);
      this.getTours();
    });
  }
}
