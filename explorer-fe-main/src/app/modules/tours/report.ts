export interface Report {
  authorId: number;
  date: Date;
  soldToursCount: number;
  totalProfit: number;
  salesIncreasePercentage: string;
  topSellingTourId: number;
  topSellingTourCount: number;
  leastSellingTourId: number;
  leastSellingTourCount: number;
}
