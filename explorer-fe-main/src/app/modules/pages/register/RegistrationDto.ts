export class RegistrationDto {
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  password: string;
  interests: string[];

  constructor(
    firstName: string = '',
    lastName: string = '',
    username: string = '',
    email: string = '',
    password: string = '',
    interests: string[] = []
  ) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.username = username;
    this.email = email;
    this.password = password;
    this.interests = interests;
  }
}
