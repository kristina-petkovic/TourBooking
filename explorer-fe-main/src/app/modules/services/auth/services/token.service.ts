import {Injectable} from '@angular/core';
import {Token} from "../models/token";
import jwtDecode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() {
  }


  getToken(): string | null {
    if (window.sessionStorage.getItem("token") == "") {
      return "";
    } else {
      return window.sessionStorage.getItem("token");
    }
  }

  getIdFromToken(): string {
    let token = window.sessionStorage.getItem("token")
    if (token) {
      const decodedToken: Token = jwtDecode(token);
      return decodedToken.nameid;
    }
    return '';
  }

  getRole(): string | null {
    if (window.sessionStorage.getItem("role") == "") {
      return "";
    } else {
      return window.sessionStorage.getItem("role");
    }
  }

  getId(): string | null {
    if (window.sessionStorage.getItem("id") == "") {
      return "";
    } else {
      return window.sessionStorage.getItem("id");
    }
  }


  getEmailFromToken(): string {
    let token = window.localStorage.getItem("token")
    if (token) {
      let tokenSplit = token.split('.')
      let decoded = decodeURIComponent(encodeURIComponent(window.atob(tokenSplit[1])));
      let obj = JSON.parse(decoded);
      return obj['Email'];
    }
    return '';
  }

  getRoleFromToken(): string {
    let token = window.sessionStorage.getItem("token")
    if (token) {
      const decodedToken: Token = jwtDecode(token);
      console.log(decodedToken)
      return decodedToken.role;
    }
    return '';
  }
}
