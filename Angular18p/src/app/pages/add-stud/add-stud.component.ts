import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-stud',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-stud.component.html',
  styleUrl: './add-stud.component.css'
})
export class AddStudComponent {

  formTempModel:any;
  stundentlist:any={
    firstname:'',
    lastname:'',
    city:'',
    zincode:'',
    password:''
  }
  constructor(){
  }

  submitform(){
    this.formTempModel=this.stundentlist
  }
}
