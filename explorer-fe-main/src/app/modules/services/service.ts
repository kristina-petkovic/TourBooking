import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth/services/auth.service';
import {RegistrationDto} from "../pages/register/RegistrationDto";

@Injectable({
  providedIn: 'root',
})
export class Service {
  private baseUrl = 'http://localhost:16177/api';

  private headers: HttpHeaders;

  constructor(private http: HttpClient, private authService: AuthService) {
    this.headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
  }

  private getHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    if (token) {
      return this.headers.set('Authorization', `Bearer ${token}`);
    }
    return this.headers;
  }

  // User Endpoints
  registerUser(dto: RegistrationDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/user/userRegistration`, dto);
  }

  blockUser(userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/user/block${userId}`, {});
  }

  unblockUser(userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/user/unblock${userId}`, {});
  }

  getAllUsers(): Observable<any> {
    return this.http.get(`${this.baseUrl}/user`);
  }
  getUserById(id: number):Observable<Object>{
    return this.http.get(`${this.baseUrl}/user/get${id}`);
  }

  // Report Endpoint
  createReport(authorId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/report/report${authorId}`, {});
  }
  // Purchase Endpoints
  buyToursFromCart(cartDto: any): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/purchase/buy`, cartDto, { headers: this.getHeaders() });
  }

  getAllPurchasesByTouristId(touristId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/purchase/id${touristId}`);
  }

  // KeyPoint Endpoint
  createKeyPoint(dto: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/keypoint/createkeypoint`, dto, { headers: this.getHeaders() });
  }




  //INTERESTS
  createUserInterest(interest: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/interest/newUserInterest`, interest, { headers: this.getHeaders() });
  }

  deleteUserInterest(id: any): Observable<any> {
    const options = {
      headers: this.getHeaders()
    };
    return this.http.post(`${this.baseUrl}/interest/delete${id}`, options);
  }

  getAllInterestsByTouristId(touristId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/interest/tourist?id=${touristId}`);
  }

  getAllInterestsByTourId(tourId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/interest/tour?id=${tourId}`);
  }

  //CART

  // New Cart-related methods
  addToCart(dto: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/cart/createtour`, dto, { headers: this.getHeaders() });
  }

  getCartByTouristId(touristId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/cart/tourist${touristId}`, {}, { headers: this.getHeaders() });
  }

  deleteCartItem(id: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/cart/deleteitem${id}`, {}, { headers: this.getHeaders() });
  }
  // Tour Endpoints
  createTour(dto: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/tour/createtour`, dto);
  }

  publishTour(tourId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/tour/publish${tourId}`, {});
  }

  archiveTour(tourId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/tour/archive${tourId}`, {});
  }

  getTourById(tourId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/tour/get${tourId}`, {});
  }

  getToursByAuthorId(authorId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/tour/author${authorId}`, {});
  }
  getAllPublishedTours(): Observable<any> {
    return this.http.get(`${this.baseUrl}/tour`);
  }

  recommendTours(touristId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/tour/recommend${touristId}`);
  }

  recommendToursByDifficulty(touristId: number, difficulty: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/tour/recommend${touristId}/difficulty${difficulty}`);
  }

  getAllToursByTopAuthors(): Observable<any> {
    return this.http.get(`${this.baseUrl}/tour/allbytop`);
  }

  filterToursByStatus(status: string): Observable<any> {
    return this.http.get(`${this.baseUrl}/tour/filter?status=${status}`);
  }

  decreaseCartItemCount(id: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/cart/decrease${id}`, {}, { headers: this.getHeaders() });
  }

  // Issue Endpoints

  createIssue(dto: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/issue/createissue`, dto, { headers: this.getHeaders() });
  }

  getAllIssuesByAuthorId(authorId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/issue/author${authorId}`, { headers: this.getHeaders() });
  }

  resolveIssue(issueId: number, userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/issue/resolve${issueId}/loggeduser${userId}`, {}, { headers: this.getHeaders() });
  }

  reviseIssue(issueId: number, userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/issue/revision${issueId}/loggeduser${userId}`,  { headers: this.getHeaders() });
  }

  declineIssue(issueId: number, userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/issue/decline${issueId}/loggeduser${userId}`, {}, { headers: this.getHeaders() });
  }

  getAllRevision(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/issue/admin${id}`, { headers: this.getHeaders() });
  }

  getAllTouristIssues(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/issue/tourist${id}`, { headers: this.getHeaders() });
  }

  pendingAgain(issueId: number, userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/issue/pending${issueId}/loggeduser${userId}`, {}, { headers: this.getHeaders() });
  }

  getReportByAuthorId(number: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/report/get${number}`, {});

  }
}
