import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DateValueAccessor } from './date-value-accessor';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [DateValueAccessor],
  exports: [DateValueAccessor]
})
export class DateValueAccessorModule { }
