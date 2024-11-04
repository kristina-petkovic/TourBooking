export interface IssueDTO {
  id: number;
  name: string;
  authorId: number;
  tourId: number;
  touristId: number;
  adminId: number;
  text: string;
  status: number;
}
export enum IssueStatus {
  Pending = 'Pending',
  Revision = 'Revision',
  Declined = 'Declined',
  Resolved = 'Resolved'
}

export class Issue {
  id: number;
  name: string;
  authorId: number;
  adminId: number;
  tourId: number;
  touristId: number;
  text: string;
  status: number;
  createdAt: Date;
  updatedAt?: Date | null;

  constructor( id: number,
    authorId: number,
    name: string,
    adminId: number,
    tourId: number,
    touristId: number,
    text: string,
    status: number,
    createdAt: Date,
    updatedAt?: Date | null
  ) {
    this.id = id;
    this.authorId = authorId;
    this.adminId = adminId;
    this.tourId = tourId;
    this.touristId = touristId;
    this.text = text;
    this.name = name;
    this.status = status;
    this.createdAt = createdAt;
    this.updatedAt = updatedAt ?? null;
  }
}
