import { Component, OnInit } from '@angular/core';
import {Service} from "../../../services/service";
import {Issue} from "../../issueDTO";
import {AuthService} from "../../../services/auth/services/auth.service";
import {TokenService} from "../../../services/auth/services/token.service";
import {TourDto} from "../../tour.model";

@Component({
  selector: 'app-issues',
  templateUrl: './issues.component.html',
  styleUrls: ['./issues.component.css']
})
export class IssuesComponent implements OnInit {

  issues: Issue[] = [];
  AuthorLoggedIn: boolean = false;
  AdminLoggedIn: boolean = false;
  TouristLoggedIn: boolean = false;
  id: string | null  = '';

  constructor(private service: Service, private auth: AuthService, private ts:TokenService) {}

  ngOnInit(): void {

    this.id = this.auth.getId()
    this.isLoggedAuthor()
    this.isLoggedAdmin()
    this.isLoggedTourist()
    this.loadIssues();
  }

  isLoggedAdmin(): boolean {
    this.AdminLoggedIn = this.ts.getRole() === '2';
    return this.AdminLoggedIn;
  }
  isLoggedAuthor(): boolean {
    this.AuthorLoggedIn = this.ts.getRole() === '1';
    return this.AuthorLoggedIn;
  }

  isLoggedTourist(): boolean {
    this.TouristLoggedIn = this.ts.getRole() === '0';
    return this.TouristLoggedIn;
  }

  loadIssues(): void {
    if (this.isLoggedAuthor()) {
      this.service.getAllIssuesByAuthorId(Number(this.id)).subscribe(i => {
        this.issues = i;
      });
    }
    if (this.isLoggedAdmin()) {
      this.service.getAllRevision(Number(this.id)).subscribe(i => {
        this.issues = i;
      });
    }
    if (this.isLoggedTourist()) {
      this.service.getAllTouristIssues(Number(this.id)).subscribe(i => {
        this.issues = i;
      });
    }
  }
  getStatusLabel(status: number): string {
    switch(status) {
      case 0:
        return 'Pending';
      case 1:
        return 'Revision';
      case 2:
        return 'Resolved';
      default:
        return 'Declined';
    }
  }

  reviseIssue(issue: Issue): void {

    this.service.reviseIssue(issue.id,Number(this.id)).subscribe(updatedIssue => {
      this.loadIssues();  // Refresh issues list after update
    });
  }

  declineIssue(issue: Issue): void {
    this.service.declineIssue(issue.id,Number(this.id)).subscribe(updatedIssue => {
      this.loadIssues();  // Refresh issues list after update
    });
  }

  pendingIssue(issue: Issue): void {
    this.service.pendingAgain(issue.id,Number(this.id)).subscribe(updatedIssue => {
      this.loadIssues();
    });
  }

  //resolveIssue
  resolveIssue(issue: Issue): void {
    this.service.resolveIssue(issue.id ,Number(this.id)).subscribe(updatedIssue => {
      this.loadIssues();
    });
  }

}
