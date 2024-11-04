import {CommonModule} from "@angular/common";
import {NgModule} from "@angular/core";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterModule, Routes} from "@angular/router";
import {MaterialModule} from "src/app/material/material.module";
import {NavbarModule} from "./navbar/navbar.module";
import { FooterComponent } from './footer/footer.component';
import {StatePipePipe} from "../services/state-pipe.pipe";
import { CreateComponent } from './tour/create/create.component';
import { AllToursComponent } from './tour/all-tours/all-tours.component';
import { CartComponent } from './tour/cart/cart.component';
import { ReportComponent } from './tour/report/report.component';
import { TourSuggestionsComponent } from './tour/tour-suggestions/tour-suggestions.component';
import { AllUsersViewComponent } from './tour/all-users-view/all-users-view.component';
import { KeyPointFormComponent } from './tour/key-point-form/key-point-form.component';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CreateIssueComponent } from './tour/create-issue/create-issue.component';
import { IssuesComponent } from './tour/issues/issues.component';
import { OneTourViewComponent } from './tour/one-tour-view/one-tour-view.component';




const routes: Routes = []

@NgModule({
  declarations: [
    StatePipePipe,
    FooterComponent,
    CreateComponent,
    AllToursComponent,
    CartComponent,
    ReportComponent,
    TourSuggestionsComponent,
    AllUsersViewComponent,
    KeyPointFormComponent,
    AllToursComponent,
    CreateIssueComponent,
    IssuesComponent,
    OneTourViewComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    NavbarModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  exports: [RouterModule, FooterComponent]
})
export class TourModule {
}
