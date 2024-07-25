import { Component } from '@angular/core';
import { FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzIconModule, NzIconService } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { UserService } from '../../service/userData.service';
import { Router, RouterLink } from '@angular/router';
import {NzSelectModule} from 'ng-zorro-antd/select';
import { GriddetailgridService } from '../../service/griddetailgrid.service';
import { NzUploadFile, NzUploadModule } from 'ng-zorro-antd/upload';
import {NzModalModule  } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule,NzButtonModule,NzFormModule,NzIconModule,NzInputModule,NzCheckboxModule,
    RouterLink,
    NzSelectModule,
    NzUploadModule,
    NzModalModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  loginForm: FormGroup;
  passwordVisible = false;
  password?: string;

    constructor(private fb: NonNullableFormBuilder,
      private userData:UserService,
      private router:Router
    ) {
      this.loginForm= this.fb.group({
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        userName: ['', [Validators.required]],
        email: ['', [Validators.required,Validators.email]],
        password: ['', [Validators.required]],
        phone: ['', [Validators.required]],
        isActive:[true],
        images:[[]]
      });
    }
  submitForm(): void {
    console.log(this.loginForm.value);
    
    if (this.loginForm.valid) {
      this.userData.registerUser(this.loginForm.value).subscribe(
        response => {

          if (response.isSuccess) {
           this.userData.ShowNotification("success", "", 'User successfully registered');
            this.router.navigate(['/login']);
            this.loginForm.reset();
          }

      },
    )
  } else {
      Object.values(this.loginForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }
  
}
