import {Interest} from "./interest";

export interface KeyPoint {
  tourId: number;
  order: number;
  name: string;
  description: string;
  image: string;
  latitude: number;
  longitude: number;
}

export interface TourDto {
  name: string;
  description: string;
  difficulty: string;  // Adjust based on your enum
  interests: Interest[];
  price: number;
  authorId: number;
  keyPoints: KeyPoint[];
  status: string;  // Adjust based on your enum
  ticketCount: number;
  id: number;
  noSalesInLastThreeMonths: boolean
}

export interface Tour{
  name: string;
  description: string;
  difficulty: string;  // Adjust based on your enum
  interests: Interest[];
  price: number;
  authorId: number;
  status: string;  // Adjust based on your enum
  ticketCount: number;
  id: number;
  NoSalesInLastThreeMonths: boolean
}
