import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth/services/auth.service";
import {Router} from "@angular/router";
import {Service} from "../../services/service";
import {MatTableDataSource} from "@angular/material/table";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public dataSource = new MatTableDataSource<any>();
  public displayedColumns = ['name', 'companyName', 'quantity' ];

  public allEquipment = new MatTableDataSource<any>();

  constructor(private Service: Service, private router: Router) {}

  ngOnInit(): void {

   /* this.Service.getAllEquipment().subscribe((data: any) => {
      this.dataSource = data;
      this.allEquipment = this.dataSource;
    });*/
  }

}
