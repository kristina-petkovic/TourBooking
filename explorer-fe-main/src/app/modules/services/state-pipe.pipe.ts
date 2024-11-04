import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statePipe'
})
export class StatePipePipe implements PipeTransform {
  transform(value: number): string {

    if (value === 0) {
      return 'DONE';
    } else if (value === 1) {
      return 'BOOKED';
    } else {
      return 'CANCELED';
    }
  }
}
