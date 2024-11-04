import { Component, OnInit } from '@angular/core';
import {Service} from "../../../services/service";
import {UserModel} from "../../user.model";
import {User} from "../../../services/auth/models/user";

@Component({
  selector: 'app-all-users-view',
  templateUrl: './all-users-view.component.html',
  styleUrls: ['./all-users-view.component.css']
})
export class AllUsersViewComponent implements OnInit {


  users: UserModel[] = [];
  blocked: boolean = false;
  constructor(private userService: Service) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getAllUsers().subscribe((data: UserModel[]) => {
    // Sort users by the 'role' field
    this.users = data.sort((a, b) => a.role.toString().localeCompare(b.role.toString()));
  });
  }

  getRoleLabel(role: string): string {
    switch(role) {
      case "0":
        return 'Tourist';
      case "1":
        return 'Author';
      case "2":
        return 'Admin';
      default:
        return 'Unknown';
    }
  }

  block(user: UserModel): void {
    this.userService.blockUser(user.id).subscribe();
    this.loadUsers();
  }
  unblock(user: UserModel): void {
    this.userService.unblockUser(user.id).subscribe();
    this.loadUsers();
  }
}
