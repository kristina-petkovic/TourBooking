import { Component, OnInit } from '@angular/core';
import * as L from "leaflet";
import { Service } from "../../../services/service";
import { AuthService } from "../../../services/auth/services/auth.service";
import { MatDialog } from '@angular/material/dialog';
import { KeyPointFormComponent } from '../key-point-form/key-point-form.component'; // Import your form component

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  map: L.Map | undefined;
  tourDto = {
    name: '',
    ticketCount: 0,
    description: '',
    difficult: 'easy',
    authorId: 0,
    price: 0,
    interests: [] as string[],
    keyPoints: [] as {
      latitude: number,
      longitude: number,
      filepath?: string,
      name?: string,
      description?: string,
      image?: string,
      order?: number
    }[]
  };
  initialMarker: L.Marker | undefined; // Store the initial marker
  selectedKeyPoint: { latitude: number, longitude: number, filepath?: string, name?: string, description?: string, image?: string , order?: number} | undefined;
  keyPointOrder: number = 1;

  constructor(private service: Service, private auth: AuthService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.map = L.map('map').setView([45.2233, 19.8836], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19
    }).addTo(this.map);

    this.initialMarker = L.marker([45.2233, 19.8836]).addTo(this.map);
    this.initialMarker.bindPopup('<b>Fruška Gora</b>').openPopup();

    this.map.on('click', (e: L.LeafletMouseEvent) => {
      if (this.map) {
        if (this.initialMarker) {
          this.map.removeLayer(this.initialMarker);
          this.initialMarker = undefined; // Ensure it's no longer referenced
        }
        this.handleMapClick(e.latlng);
      }
    });
  }

  handleMapClick(latLng: L.LatLng): void {
    if (this.map) { // Ensure map is initialized
      if (this.initialMarker) {
        this.map.removeLayer(this.initialMarker);
        this.initialMarker = undefined;
      }

      this.selectedKeyPoint = {
        latitude: latLng.lat,
        longitude: latLng.lng,
        name: '',
        description: '',
        image: '',
        order: this.keyPointOrder
      };

      // Show form for keypoint details
      this.showKeyPointForm();
    }
  }

  showKeyPointForm(): void {
    const dialogRef = this.dialog.open(KeyPointFormComponent, {
      width: '400px',
      data: this.selectedKeyPoint
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.selectedKeyPoint = result;
        this.saveKeyPoint();
      }
    });
  }

  saveKeyPoint(): void {
    if (this.selectedKeyPoint) {
      if (!this.map) {
        console.error('Map is not initialized.');
        return;
      }

      this.tourDto.keyPoints.push(this.selectedKeyPoint);


      console.log(this.tourDto.keyPoints)
      L.marker([this.selectedKeyPoint.latitude, this.selectedKeyPoint.longitude])
        .addTo(this.map)
        .bindPopup(`
        <b>${this.selectedKeyPoint.name || 'New Key Point'}</b><br>
        ${this.selectedKeyPoint.order || 0}<br>
        ${this.selectedKeyPoint.description || ''}<br>
        ${this.selectedKeyPoint.image ? `<img src="${this.selectedKeyPoint.image}" alt="Image" style="width:100px;height:auto;">` : ''}
      `)
        .openPopup();

      this.keyPointOrder++;
      this.selectedKeyPoint = undefined;
    }
  }

  onCheckboxChange(event: any) {
    const interest = event.target.value;
    if (event.target.checked) {
      this.tourDto.interests.push(interest);
    } else {
      this.tourDto.interests = this.tourDto.interests.filter(i => i !== interest);
    }
  }

  onSubmit(): void {
    if (!this.tourDto.name || !this.tourDto.description || !this.tourDto.price) {
      alert("You have to fill out fields!");
      return;
    }
    this.tourDto.authorId = Number(this.auth.getId());

    this.service.createTour(this.tourDto).subscribe(response => {
      console.log('Tour created successfully:', response);
      const tourId = response.id;

      alert("successfully created tour with id:"+tourId)
      if (this.tourDto.keyPoints.length > 0) {
        this.tourDto.keyPoints.forEach(keyPoint => {
          const keyPointDto = { ...keyPoint, tourId: tourId };

          console.log(keyPointDto)
          this.service.createKeyPoint(keyPointDto).subscribe(kpResponse => {
            console.log('Key Point created successfully:', kpResponse);

          }, error => {
            console.error('Error creating key point:', error);
          });
        });
      }
    }, error => {
      console.error('Error creating tour:', error);
    });
  }
}
