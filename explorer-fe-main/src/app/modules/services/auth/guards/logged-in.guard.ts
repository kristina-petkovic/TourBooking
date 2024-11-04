import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {TokenService} from "../services/token.service";
import {AuthService} from "../services/auth.service";

@Injectable({
  providedIn: 'root'
})
export class LoggedInGuard {

  constructor(private authService: AuthService, private ts: TokenService, private router: Router) {}


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (this.ts.getRole() === '1' || this.ts.getRole() === '2' || this.ts.getRole() === '0') {
      return true;
    } else {
      alert('You have to log in to see this page')
      this.router.navigate(['/login'])

      return false;
    }
  }
}
