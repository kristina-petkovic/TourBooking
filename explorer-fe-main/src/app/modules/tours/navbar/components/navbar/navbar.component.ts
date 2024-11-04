import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {HttpClient} from "@angular/common/http";
import {TokenService} from "../../../../services/auth/services/token.service";
import {AuthService} from "../../../../services/auth/services/auth.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  private userType: number = -1;
  isToggled: boolean = false
  name: string = ''
  id: number = 0;


  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
    private router: Router,
    private auth: AuthService) {
  }

  token = localStorage.getItem("token")
  email: string = '';
  loggedOut: boolean = true;

  ngOnInit(): void {
    this.id = Number(this.tokenService.getId());
    this.userType = Number(this.tokenService.getRole());
  }


  isLoggedUser() {
    return this.auth.getToken() !== null;
  }

  isLoggedTourist(): boolean {
    return this.userType == 0;
  }

  isLoggedAuthor(): boolean {
    return this.userType == 1;
  }

  isLoggedAdmin(): boolean {
    return this.userType == 2;
  }

  onLogout() {
    this.auth.clearAuthAndRedirectHome();
    localStorage.clear();
    this.loggedOut = true;
  }

  onToggle() {
    this.isToggled = !this.isToggled;
  }







  Tours() {
    this.router.navigate(['/all-tours' ])
  }

  TourSuggestions() {
    this.router.navigate(['/tour/suggestions'])
  }

  Cart() {
    this.router.navigate(['/cart' ])
  }

  Users() {
    this.router.navigate(['/all-users'])
  }
  CreateIssue() {
    this.router.navigate(['/create-issue'])
  }
  Issue() {
    this.router.navigate(['/issues'])
  }


  Login() {
    this.router.navigate(['/login'])
  }

  Register() {
    this.router.navigate(['/register'])
  }

  Report() {
    this.router.navigate(['/report'])
  }


  Home() {
    if (this.isLoggedUser()) {
      this.router.navigate(['/landing'])
    } else {
      this.router.navigate(['/'])
    }
  }

  CreateTour() {
    this.router.navigate(['/create-tour'])
  }
}

