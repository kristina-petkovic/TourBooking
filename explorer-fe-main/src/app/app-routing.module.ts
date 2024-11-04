import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {HomeComponent} from "./modules/pages/home/home.component";
import {LogInComponent} from "./modules/pages/log-in/log-in.component";
import {RegisterComponent} from "./modules/pages/register/register.component";
import {LandingPageComponent} from "./modules/pages/landing-page/landing-page.component";
import {HasAuthorGuard} from "./modules/services/auth/guards/has-author-guard.service";
import {HasTouristGuard} from "./modules/services/auth/guards/has-tourist-guard.service";
import {LoggedInGuard} from "./modules/services/auth/guards/logged-in.guard";
import {TourSuggestionsComponent} from "./modules/tours/tour/tour-suggestions/tour-suggestions.component";

import {ReportComponent} from "./modules/tours/tour/report/report.component";
import {CreateComponent} from "./modules/tours/tour/create/create.component";
import {AllUsersViewComponent} from "./modules/tours/tour/all-users-view/all-users-view.component";
import {AllToursComponent} from "./modules/tours/tour/all-tours/all-tours.component";
import {HasAdminGuard} from "./modules/services/auth/guards/has-admin-guard.service";
import {CartComponent} from "./modules/tours/tour/cart/cart.component";
import {CreateIssueComponent} from "./modules/tours/tour/create-issue/create-issue.component";
import {IssuesComponent} from "./modules/tours/tour/issues/issues.component";
import {OneTourViewComponent} from "./modules/tours/tour/one-tour-view/one-tour-view.component";

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LogInComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'landing', component: LandingPageComponent, canActivate: [LoggedInGuard]},
  {path: 'tour/suggestions', component: TourSuggestionsComponent, canActivate: [HasTouristGuard]},
  {path: 'tour-details/:id', component: OneTourViewComponent},
  {path: 'report', component: ReportComponent, canActivate: [HasAuthorGuard]},
  {path: 'create-tour', component: CreateComponent, canActivate: [HasAuthorGuard]},
  {path: 'all-users', component: AllUsersViewComponent, canActivate: [HasAdminGuard]},
  {path: 'all-tours', component: AllToursComponent},
  {path: 'cart', component: CartComponent, canActivate: [HasTouristGuard]},
  {path: 'create-issue', component: CreateIssueComponent, canActivate: [HasTouristGuard]},
  {path: 'issues', component: IssuesComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
