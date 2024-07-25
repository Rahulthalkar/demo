import { Component, OnInit, signal } from '@angular/core';
import { UserService } from '../../service/userData.service';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { Router, RouterLink } from '@angular/router';
@Component({
  selector: 'app-profile-edit',
  standalone: true,
  imports: [NzFormModule,NzButtonModule,ReactiveFormsModule,NzInputModule,RouterLink],
  templateUrl: './profile-edit.component.html',
  styleUrl: './profile-edit.component.css'
})
export class ProfileEditComponent implements OnInit{
  fullName=signal('');
  email=signal('');
  userViewModel:any;
  profileUpdateForm:FormGroup;
  userValidId=signal(0);
  constructor(private userService:UserService,
    private fb:FormBuilder,private router:Router
  ){
    const user= sessionStorage.getItem('valid-user');
    if(user){
       const userId=JSON.parse(user)
       this.userValidId.set(userId.id)
    }
    this.profileUpdateForm=fb.group({
      id:this.userValidId(),
      firstName:['',[Validators.required]],
      lastName:['',[Validators.required]],
      userName:['',[Validators.required]],
      email:['',[Validators.required]],
      phone:['',[Validators.required]]
    })
  }
  ngOnInit(): void {
      this.userService.GetUserDetailById(this.userValidId()).subscribe({
        next:(response:any)=>{
          if(response.isSuccess){
            this.userViewModel=response.value;
            this.fullName.set(`${response.value.firstName} ${response.value.lastName} `);
            this.email.set(`${response.value.email}`);
            console.log(response.value);
            
            this.profileUpdateForm.patchValue({
              firstName:response.value.firstName,
              lastName:response.value.lastName,
              userName:response.value.userName,
              email:response.value.email,
              phone:response.value.mobileNo,
            });
            
          }
        }
      }) 
  }

  updateProfile(): void {
    const mobileno=(this.profileUpdateForm.get('phone')?.value);
    this.profileUpdateForm.patchValue({
      phone:String(mobileno)
    })
console.log(this.profileUpdateForm.value);

    if (this.profileUpdateForm.valid) {
console.log('userdata',this.profileUpdateForm.value);

      this.userService.UpdateEmployees(this.profileUpdateForm.value).subscribe({
        next:(response:any)=>{
          if(response.isSuccess){
            this.userService.ShowNotification(
              'success','','User updated successfully'
            );
            this.router.navigate(['/my-profile']);
          }
        }
      })
    } else {
      Object.values(this.profileUpdateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

}
