import { Component, OnInit } from '@angular/core';
import {TourDto} from "../../tour.model";
import {Service} from "../../../services/service";
import {AuthService} from "../../../services/auth/services/auth.service";
import {Purchase} from "../../purchase";
import {IssueDTO, IssueStatus} from "../../issueDTO";

@Component({
  selector: 'app-create-issue',
  templateUrl: './create-issue.component.html',
  styleUrls: ['./create-issue.component.css']
})
export class CreateIssueComponent implements OnInit {

  purchases: Purchase[] = [];
  id: string | null  = '';

  showIssueForm: boolean = false;
  issueDTO: IssueDTO = {
    id: 0,
    name: '',
    authorId: 0,
    tourId: 0,
    touristId: 0,
    adminId: 0,
    text: '',
    status: 0
  };
  issueStatuses = Object.values(IssueStatus); // Get all possible status values

  constructor(private service: Service, private auth: AuthService) {}

  ngOnInit(): void {

    this.id = this.auth.getId()
    this.get();
  }
  toggleIssueForm(): void {
    this.showIssueForm = !this.showIssueForm;
  }


  get(): void {

      this.service.getAllPurchasesByTouristId(Number(this.id)).subscribe(tours => {
        this.purchases = tours;
      });
    }



  createIssue(purchase: any) {
    this.issueDTO.touristId= Number(this.id);
    this.issueDTO.tourId = purchase.tourId;
    this.issueDTO.authorId = purchase.authorId;
    this.showIssueForm = true;
  }

  submit() {

    if (!this.issueDTO.text) {
      alert("You have to fill out text field!");
      return;
    }
    this.service.createIssue(this.issueDTO).subscribe(tours => {
      alert('issue created')
      this.showIssueForm = false;
    });
  }
}
