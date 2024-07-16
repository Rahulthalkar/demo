import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzIconModule, NzIconService } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { RouterLink } from '@angular/router';
import { UserService } from '../../service/userData.service';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-update-password',
  standalone: true,
  imports: [ReactiveFormsModule,NzButtonModule,NzFormModule,NzIconModule,NzInputModule,NzCheckboxModule,
    RouterLink
  ],
  templateUrl: './update-password.component.html',
  styleUrl: './update-password.component.css'
})
export class UpdatePasswordComponent {

  updateForm!: FormGroup;
  currentpasswordVisible = false;
  newpasswordVisible = false;
  conpasswordVisible = false;
  password?: string;
  isUserId:string;
  emailId:string;
  constructor(private fb: NonNullableFormBuilder,
    private userService:UserService
  ) {
    this.isUserId = (sessionStorage.getItem('userId') || '');
    this.emailId =sessionStorage.getItem('userEmail')|| '';

    this.updateForm= this.fb.group({
     
      currentpassword: ['', [Validators.required]],
      newpassword: ['', [Validators.required,this.newValidator]],
      confirmpassword: ['',[Validators.required, this.confirmValidator]],
    
    });
  }
    validateConfirmPassword(): void {
      Promise.resolve().then(() => this.updateForm.controls['confirmpassword'].updateValueAndValidity());
    }
    confirmValidator: ValidatorFn = (control: AbstractControl): { [s: string]: boolean } => {
      if (!control.value) {
        return { error: true, required: true };
      } else if (control.value !== this.updateForm.controls!['newpassword'].value) {
        return { confirmpassword: true, error: true };
      }
      return {};
    };

    submitForm(): void {
      if (this.updateForm.valid) {
        const user:any={
          userId:this.isUserId,
          Password:this.updateForm.get('newpassword')?.value
        }
        console.log(user);
        
        this.userService.UpdatePassword(user).subscribe({
          next:(response:any)=>{
            if (response.isSuccess) {
              this.userService.ShowNotification('success', '','PasswordIsUpdatedSuccessfully');
            } else {
              if (response.errorMessageKey == 'UnableToUpdatePassword') {
                this.userService.ShowNotification('error', '','UnableToUpdatePassword');
              }
            }
          },error: (error: HttpErrorResponse) => {
            this.userService.ShowNotification('error', '','UnableToUpdatePassword');
          },
        })
      } else {
        Object.values(this.updateForm.controls).forEach(control => {
          if (control.invalid) {
            control.markAsDirty();
            control.updateValueAndValidity({ onlySelf: true });
          }
        });
      }
    }
    
         // Validator function to check if old password and new password are the not same
  newValidator: ValidatorFn = (control: AbstractControl): { [s: string]: boolean } => {
    const newPassword = control.value;
    const oldPasswordControl = control.parent?.get('currentpassword');

    if (!newPassword) {
      return { required: true };
    } else if (oldPasswordControl && newPassword === oldPasswordControl.value) {
      return {
        newpasswordSameAsOld: true, error: true
      };
    }
    return {};
  };

  validUserPassword(){
    const password=this.updateForm.get('currentpassword')?.value || '';
      this.userService.ValidateUser(this.emailId,password,).subscribe({
        next: (response: any) => {
          if (response.isSuccess) {
            this.userService.ShowNotification('success', '','Valid user');
            
          } else {
            this.userService.ShowNotification('error', '','Invalid current password');
          }
        },
        error: (error: HttpErrorResponse) => {
          this.userService.ShowNotification('error', '','Invalid current password');
        },
      });
    }
  //}
}
