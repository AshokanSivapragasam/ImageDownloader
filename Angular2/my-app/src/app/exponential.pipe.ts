import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'exponential'
})
export class ExponentialPipe implements PipeTransform {

  transform(value: any, factor: any): any {
    return Math.pow(value, isNaN(factor) ? 1 : factor);
  }
}
