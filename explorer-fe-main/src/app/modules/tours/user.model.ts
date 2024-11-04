export class UserModel {
  id: number;          // Assuming 'id' is required
  issueCount: number;
  malicious: boolean;
  blocked: boolean;
  authorPoints: number;
  topAuthor: boolean;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  email: string;
  role: Role;

  constructor(
    id: number,
    issueCount: number,
    malicious: boolean,
    blocked: boolean,
    authorPoints: number,
    topAuthor: boolean,
    username: string,
    password: string,
    firstName: string,
    lastName: string,
    email: string,
    role: Role
  ) {
    this.id = id;
    this.issueCount = issueCount;
    this.malicious = malicious;
    this.blocked = blocked;
    this.authorPoints = authorPoints;
    this.topAuthor = topAuthor;
    this.username = username;
    this.password = password;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.role = role;
  }
}

// Assuming Role is an enum, define it similarly to your .NET Role enum
export enum Role {
  Admin = 'Admin',
  Tourist = 'Tourist',
  Author = 'Author'
}

