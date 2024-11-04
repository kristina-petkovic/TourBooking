import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth/services/auth.service";
import {Router} from "@angular/router";
import {Service} from "../../services/service";
import {RegistrationDto} from "./RegistrationDto";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registrationDto: RegistrationDto = {
    firstName: '',
    lastName: '',
    username: '',
    email: '',
    password: '',
    interests: []
  };



  constructor(private authService: AuthService,
              private Service: Service,
              private router: Router) {
  }


  updateInterests(event: any, interest: string) {
    if (event.target.checked) {
      this.registrationDto.interests.push(interest);
    } else {
      const index = this.registrationDto.interests.indexOf(interest);
      if (index > -1) {
        this.registrationDto.interests.splice(index, 1);
      }
    }
  }
  register() {

    if (!this.registrationDto.email || !this.registrationDto.password || !this.registrationDto.username) {
      alert("You have to fill out email, password and username fields!");
      return;
    }
    console.log(this.registrationDto)

      this.Service.registerUser(this.registrationDto).subscribe({
        next: res => {
          console.log(res);
          alert("Successfully registered user with id: " + res.id );
          document.querySelector('.success-message')!.textContent = 'User registered successfully';

        },
        error: err => {
          if (err.status === 400) {
            alert('Error');
          }
        }
      });
  }

  ngOnInit(): void {

  }

  login() {
    this.router.navigate(['/login']);
  }

}
