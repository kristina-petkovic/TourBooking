export class LoginResponse {
  token: string;
  id: number;
  role: number;


  constructor(token: string, id: number, role: number) {
    this.token = token
    this.id = id
    this.role = role
  }
}
