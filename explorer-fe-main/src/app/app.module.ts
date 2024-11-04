import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {AppRoutingModule} from "./app-routing.module";
import {AppComponent} from "./app.component";
import {MaterialModule} from "./material/material.module";
import {TourModule} from "./modules/tours/tour.module";
import {PagesModule} from "./modules/pages/pages.module";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { StatePipePipe } from './modules/services/state-pipe.pipe';
import {AuthInterceptor} from "./modules/services/auth/interceptors/auth.interceptor";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    PagesModule,
    TourModule,
    MatDialogModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule {
}
