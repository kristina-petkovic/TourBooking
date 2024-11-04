export class Interest {
  id: number;
  interestTypeName: number;
  touristId: number;
  tourId: number;

  constructor(interestTypeName: number, touristId: number, tourId: number, id: number) {
    this.interestTypeName = interestTypeName;
    this.touristId = touristId;
    this.tourId = tourId;
    this.id = id;
  }
}
export enum InterestType {
  nature, art, sport, shopping, food
}
