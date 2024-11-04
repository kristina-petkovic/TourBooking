import {Injectable} from "@angular/core";
import {User} from "../models/user";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {LoginResponse} from "../dtos/login-response";
import {Router} from "@angular/router";
import {Token} from "../models/token";
import {Observable, Subject, Subscription, throwError} from "rxjs";
import {catchError} from "rxjs/operators";
import jwtDecode from "jwt-decode";
import {Users} from "../models/users";
import {TokenService} from "./token.service";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private user$: Subject<User | null> = new Subject();
  private user: User | null = null;
  headers: HttpHeaders = new HttpHeaders({
    "Content-Type": "application/json",
  });

  private token$: Subject<string | null> = new Subject();
  private token: string | null = null;

  constructor(private http: HttpClient, private router: Router, private ts: TokenService) {
    this.loadAuth();
  }

  signIn(signInRequest: any): Subscription {
    return this.http.post<any>("http://localhost:16177/api/Authentication/login", signInRequest)
      .pipe(
        catchError(error => {
          if (error.status === 400) {
            alert('');
          }
          if (error.message()=='User blocked'){
            alert('User blocked');
          }
          return this.handleError(error);
        })
      )
      .subscribe((response: any) => {
        if (response) {
          console.log(response)

          this.saveUserAndToken(response);
          this.router.navigate(['/landing'])
        }
      });
  }


  loadAuth() {
    if (this.token && this.user) {
      return;
    }
    this.loadUser();
    this.loadToken();
  }


  private saveUserAndToken(response: LoginResponse) {
    const token = response.token;
    const id = response.id;
    const role = response.role;
    const decodedToken: Token = jwtDecode(token);
    const role2 = this.getRoleToString();


    window.sessionStorage.setItem("token", token);
    window.sessionStorage.setItem("id",String(id));
    window.sessionStorage.setItem("role", String(role));


    this.token = token;
    this.token$.next(this.token);

    this.user$.next(this.user);
  }


  clearAuth() {
    this.clearUser();
    this.clearToken();
  }

  getToken() {
    return this.token;
  }

  getRoleToString(){
    console.log(this.ts.getRoleFromToken())
    return this.ts.getRoleFromToken()
  }

  isAdmin() {
    return this.ts.getRole() === '2';
  }

  clearAuthAndRedirectHome() {
    this.clearAuth();
    this.redirectHome();
  }

  private extractUser(token: string) {
    const decodedToken: Token = jwtDecode(token);
    const authorities = decodedToken.authorities.map(
      (auth: any) => auth.authority
    );
    return new User(decodedToken.sub, authorities);
  }



  private loadUser() {
    const user = window.sessionStorage.getItem("user");
    if (!user) return;

    this.user = JSON.parse(user);
    this.user$.next(this.user);
  }

  private loadToken() {
    const token = window.sessionStorage.getItem("token");
    if (!token) return;

    this.token = token;
    this.token$.next(this.token);
  }


  redirectHome() {
    this.router.navigate([""]);
  }

  public getId(): string | null {
    const id = window.sessionStorage.getItem("id");
    if (id) {
      return id;
    }
    return null;
  }

  private clearUser() {
    window.sessionStorage.removeItem("user");
    window.sessionStorage.removeItem("id");
    window.sessionStorage.removeItem("role");
    this.user = null;
    this.user$.next(this.user);
  }

  private clearToken() {
    window.sessionStorage.removeItem("token");
    this.token = null;
    this.token$.next(this.token);
  }

  private handleError(err: any) {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    if (err.error.status == 400) {
      alert('You have to finish registration');
    }
    return throwError(() => errorMessage);
  }
}
