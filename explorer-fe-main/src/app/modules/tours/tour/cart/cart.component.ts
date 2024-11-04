import { Component, OnInit } from '@angular/core';
import {CartDto} from "../../cart.model";
import {AuthService} from "../../../services/auth/services/auth.service";
import {Service} from "../../../services/service";
import {TourDto} from "../../tour.model";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: CartDto | undefined;
  id: string|null = '';

  constructor(private service: Service, private auth: AuthService) {}

  ngOnInit(): void {
    this.id = this.auth.getId();
    this.getCartDto()
  }

  getCartDto(){
    this.service.getCartByTouristId(Number(this.id)).subscribe(data => {
      this.cart = data;
    });
  }

  removeTour(tourId: number): void {
    this.service.deleteCartItem(Number(tourId)).subscribe(updatedCart => {
      this.getCartDto()

    });
  }
  removeOne(tourId: number): void {
    this.service.decreaseCartItemCount(Number(tourId)).subscribe(updatedCart => {
      this.getCartDto()

    });
  }

  purchase(cart: CartDto) {
    if(this.cart?.price == 0){
      alert('There is no tours in your cart!')
      return;
    }
    this.service.buyToursFromCart(cart).subscribe(updatedCart => {
      this.getCartDto()
      alert('Successfully bought from cart!')
    });

  }
}
