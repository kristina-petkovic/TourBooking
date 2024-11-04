// cart.model.ts
import {Tour, TourDto} from "./tour.model";

export interface CartDto {
  cartItems: CartItem[]; // Include CartItems to match the backend
  price: number;
  tourId: number;
  touristId: number;
}
// cart-item.model.ts

export interface CartItem {
  id: number,
  tour: Tour,
  touristId: number;
  tourId: number;
  count: number;
}
