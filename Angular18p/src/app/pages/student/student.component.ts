import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Observable, interval, map } from 'rxjs';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './student.component.html',
  styleUrl: './student.component.css'
})
export class StudentComponent {
  listofStudents=[
    {    id:1,firstname:'Ravi',lastname:'Patel',Active:false,totalMarks:50  },
    {    id:2,firstname:'Rajes',lastname:'Patel',Active:true ,totalMarks:78 },
    {    id:3,firstname:'Ria',lastname:'Patel',Active:false ,totalMarks:49 },
    {    id:4,firstname:'Rinkes',lastname:'Patel',Active:true  ,totalMarks:87 },
    {    id:5,firstname:'Rupa',lastname:'Patel',Active:false ,totalMarks:44  },
  ]
  isstyleClasses:any={
    'color':'red',
    'width':'100px',
    'height':'150px',
    'border-radius':'10%',
    'background-color':'green'
  }
  isnumberActive:string='';
  fistname='John thomas'
  titleview:string="this my first demo session";
  isAcitve:boolean=false;
  currentDate=new Date();
  currecntTime:Observable<Date>=new Observable<Date>


// object  json

  student:any={
    id:1,
    name:'Jack',
    city:'london'
  }

  constructor(){
    this.listofStudents
    this.currecntTime=interval(1000).pipe(map(()=>new Date()));
  }
  istoggle(){
    this.isAcitve=!this.isAcitve;
  }
 
}
