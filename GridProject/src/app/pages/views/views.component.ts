import { Component, computed, signal } from '@angular/core';
import {GoogleMap} from '@angular/google-maps';

import { NzIconModule } from 'ng-zorro-antd/icon';
@Component({
  selector: 'app-views',
  standalone: true,
  imports: [GoogleMap,NzIconModule],
  templateUrl: './views.component.html',
  styleUrl: './views.component.css'
})
export class ViewsComponent {
 price=100;
 quantity=signal(0);
 tnumber!:number;
 total=computed(()=>{
  return this.price * this.quantity()})

 plus(){
  this.quantity.update(value=>value+1);
 //this.quantity.set(this.quantity()+1);
 
 }
 minus(){

  //this.quantity.set(this.quantity()-1);
  this.quantity.update(value=>value-1);
 }
}
