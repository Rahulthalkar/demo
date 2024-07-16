import { group } from '@angular/animations';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, MinLengthValidator, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ReactiveFormsModule,],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

  studentForms:FormGroup
  formvalue:any;
  constructor(private fb:FormBuilder){
    this.studentForms=this.fb.group({
      firstname:['',Validators.required,Validators.minLength(3)],
      lastname:[''],
      username:['',Validators.required,Validators.minLength(3)],
      city:[''],
      email:['',Validators.required,Validators.email],
      phone:['',Validators.required]
    })
  }
  submitform(){
    this.formvalue=this.studentForms.value
    console.log(this.formvalue);
    
  }
}
