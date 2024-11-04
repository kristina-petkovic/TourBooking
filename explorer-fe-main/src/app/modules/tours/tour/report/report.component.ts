import { Component, OnInit } from '@angular/core';
import {Service} from "../../../services/service";
import {AuthService} from "../../../services/auth/services/auth.service";
import {TourDto} from "../../tour.model";
import {Report} from "../../report";

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {
  private id: string | null | undefined;
  report: Report | undefined;




  constructor(private service: Service, private auth: AuthService) {
  }

  ngOnInit(): void {
    this.id = this.auth.getId();
    this.loadNoSalesTours();
  }


  getReport(): void {
    this.service.createReport(Number(this.id)).subscribe((data: Report) => {
      this.report = data;
    });
    this.service.getReportByAuthorId(Number(this.id)).subscribe((data: Report) => {
      this.report = data;
    });
  }

  noSalesTours: TourDto[] = [];
  loadNoSalesTours(): void {
    this.service.getToursByAuthorId(Number(this.id)).subscribe(tours => {
      this.noSalesTours = tours.filter((tour: TourDto)  => tour.noSalesInLastThreeMonths && Number(tour.status) !==2);
    });
  }

  archiveTour(tourId: number): void {
    this.service.archiveTour(tourId).subscribe(() => {
      this.loadNoSalesTours();
    });
  }
}
