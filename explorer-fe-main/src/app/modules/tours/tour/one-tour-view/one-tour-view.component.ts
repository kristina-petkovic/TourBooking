import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Service} from "../../../services/service";
import {TourDto} from "../../tour.model";
import {CartDto} from "../../cart.model";
import {AuthService} from "../../../services/auth/services/auth.service";
import * as L from "leaflet";

@Component({
  selector: 'app-one-tour-view',
  templateUrl: './one-tour-view.component.html',
  styleUrls: ['./one-tour-view.component.css']
})
export class OneTourViewComponent implements OnInit {

  tour!: TourDto;
  id: string | null = '';
  constructor(
    private route: ActivatedRoute,
    private service: Service,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.service.getTourById(Number(id)).subscribe(tour => this.tour = tour);
    this.id = this.auth.getId();
  }
  ngAfterViewInit() {
    setTimeout(() => {
      this.initializeMap(this.tour); }, 0);
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

  getDifficultyLabel(difficulty: string) {
    if(difficulty == '0') return 'EASY'
    if(difficulty == '1') return 'MEDIUM'
    return 'HARD'
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
}
