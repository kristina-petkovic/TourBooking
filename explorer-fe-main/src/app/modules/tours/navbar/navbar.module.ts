import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterLinkActive} from "@angular/router";
import {MaterialModule} from "../../../material/material.module";
import {NavbarComponent} from "./components/navbar/navbar.component";


@NgModule({
  declarations: [
    NavbarComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    RouterLinkActive
  ],
  exports: [
    NavbarComponent
  ]
})
export class NavbarModule {
}
