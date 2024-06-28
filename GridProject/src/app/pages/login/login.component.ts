import { Component } from '@angular/core';
import { FormControl, FormGroup, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzIconModule, NzIconService } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { UserService } from '../../service/userData.service';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule,NzButtonModule,NzFormModule,NzIconModule,NzInputModule,NzCheckboxModule,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm: FormGroup;
  passwordVisible = false;
  password?: string;

    constructor(private fb: NonNullableFormBuilder,
      private userData:UserService
    ) {
      this.loginForm= this.fb.group({
        email: ['', [Validators.required,Validators.email]],
        password: ['', [Validators.required]],
        remember: [true]
      });
    }
  submitForm(): void {
    if (this.loginForm.valid) {
      this.userData.login(this.loginForm.value);
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
