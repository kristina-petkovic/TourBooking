import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AppRoutingModule} from 'src/app/app-routing.module';
import {HomeComponent} from './home/home.component';
import {LogInComponent} from './log-in/log-in.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NavbarModule} from "../tours/navbar/navbar.module";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {MaterialModule} from "../../material/material.module";
import {TourModule} from "../tours/tour.module";
import { RegisterComponent } from './register/register.component';
import { LandingPageComponent } from './landing-page/landing-page.component';

@NgModule({
  declarations: [
    HomeComponent,
    LogInComponent,
    RegisterComponent,
    LandingPageComponent,
  ],
    imports: [
        CommonModule,
        AppRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        NavbarModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MaterialModule,
        TourModule,
      MatFormFieldModule
    ]
})
export class PagesModule {
}
