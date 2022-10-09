import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../util/constants';

@Pipe({
  name: 'DateTimeFormat' // Nome que vai ser utilizado no componente
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    let day = value ? value.substring(3,5):'12';
    let year = value ? value.substring(6,10):'12';
    let hour = value ? value.substring(11,13):'12';
    let month = value ? value.substring(0,2):'12';
    let min = value ? value.substring(14,16):'12';
    value = day + '/' + month + '/' + year + ' ' + hour + ':' + min;
    return super.transform(value, Constants.DATE_TIME_FMT);
  }

}
