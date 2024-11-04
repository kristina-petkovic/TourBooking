import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {TokenService} from "../services/token.service";
import {AuthService} from "../services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class HasAdminGuard {

  constructor(private authService: AuthService, private ts: TokenService, private router: Router) {
  }

  isLoggedUser() {
    return this.authService.getToken() !== null;
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (this.ts.getRole() === '2') {
      return true;
    } else {
      alert('You dont have permission to see this page')
      if (this.isLoggedUser()) {
        this.router.navigate(['/landing'])
      } else {
        this.router.navigate(['/'])
      }
      return false;
    }
  }
}
