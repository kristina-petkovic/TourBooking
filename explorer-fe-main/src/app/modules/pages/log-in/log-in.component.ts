import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth/services/auth.service";
// @ts-ignore
import {UserDto} from "../../dto/userDto";
import {LoginRequest} from "../../services/auth/dtos/login-request";

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  username: string = ''
  password: string = ''
  loginForm!: FormGroup;
  private user: LoginRequest = new LoginRequest('', '');
  empty: boolean = false;

  constructor(private authService: AuthService,
              private router: Router) {
  }


  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.minLength(2)),
      password: new FormControl()
    })
  }

  Login() {
    this.user.password = this.password
    this.user.username = this.username
    this.empty = this.password == '' || this.username == '';

    this.authService.signIn(this.user)

  }

  register() {
    this.router.navigate(['/register']);
  }

}
