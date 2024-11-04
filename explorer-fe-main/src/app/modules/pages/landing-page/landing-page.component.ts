import { Component, OnInit } from '@angular/core';
import {AuthService} from "../../services/auth/services/auth.service";
import {Router} from "@angular/router";
import {TokenService} from "../../services/auth/services/token.service";
import {Service} from "../../services/service";

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {
  firstName: any;
  lastName: any;
  role: number = 0;
  issueCount: number = 0;
  company:any;

  constructor(private authService: AuthService,
              private router: Router, private ts: TokenService,
              private Service: Service) {
  }

  ngOnInit(): void {
    /*
    this.router.params.subscribe(params => {
      this.companyId = params['id'];
    })
    this.id = this.ts.getIdFromToken()*/
    this.Service.getUserById(Number(this.ts.getId())).subscribe((user) => {
   //   this.issueCount = user.issueCount;
    });
  }

}
