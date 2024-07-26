import { Component, EventEmitter, Output, signal, WritableSignal } from '@angular/core';
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
import { NzUploadFile, NzUploadModule, NzUploadXHRArgs } from 'ng-zorro-antd/upload';
import {NzModalModule  } from 'ng-zorro-antd/modal';
import { Observable, Observer, Subscription } from 'rxjs';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule,NzButtonModule,NzFormModule,NzIconModule,NzInputModule,NzCheckboxModule,
    RouterLink,
    NzSelectModule,
    NzUploadModule,
    NzModalModule,
    NzUploadModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  @Output() formSubmit: EventEmitter<any> = new EventEmitter<any>();
  loginForm: FormGroup;
  passwordVisible = false;
  password?: string;
  fileList: WritableSignal<NzUploadFile[]> = signal([]);
  loading: WritableSignal<boolean> = signal(false);
  avatarUrl: WritableSignal<string | undefined> = signal(undefined);
 // avatarUrl?: WritableSignal<string> = signal(undefined)
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
        photo:['']
      });
    }
  submitForm(): void {
    console.log(this.loginForm.value);
    
    if (this.loginForm.valid) {
      const formData = this.loginForm.value;
      formData.photo = this.avatarUrl();

      // this.formSubmit.emit(this.loginForm.value);
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
  
// beforeUpload = (file: NzUploadFile, _fileList: NzUploadFile[]) => {
//   return new Observable((observer: Observer<boolean>) => {
//     const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
//     if (!isJpgOrPng) {
//       this.userData.ShowNotification('error', '','onlyJPGPNGAllowed')
//       observer.complete();
//       return;
//     }
//     const isLt2M = file.size! / 1024 / 1024 < 5;
//     if (!isLt2M) {
//      this.userData.ShowNotification('error', '', 'ImageSmallerThan5')
//       observer.complete();
//       return;
//     }
//     observer.next(isJpgOrPng && isLt2M);
//     observer.complete();
//   });
// };


// customRequest = (item: NzUploadXHRArgs): Subscription => {
//   this.getBase64(item.file as any, (img: string) => {
//     this.avatarUrl.set(img)
//     this.loginForm.patchValue({ Photo: item.file });
//     console.log('ddddd=====',this.loginForm.patchValue({ Photo: item.file }));
    
//     this.fileList.set([item.file as NzUploadFile]);
//     if (item.onSuccess) {
//       item.onSuccess({}, item.file!, {});
//     }
//   });
//   return new Subscription();
// };
// private getBase64(img: File, callback: (img: string) => void): void {
//   const reader = new FileReader();
//   reader.addEventListener('load', () => callback(reader.result!.toString()));
//   reader.readAsDataURL(img);
// }

// handleChange(info: { file: NzUploadFile }): void {
//   if (info.file.status === 'uploading') {
//     this.loading.set(true)
//   }
// }
// removeImage(): void {
//   this.avatarUrl.set(undefined)
//   this.loginForm.patchValue({ Photo: null });
//   this.fileList.set([])
// }


 onFileSelected(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    const file = fileInput.files?.[0];

    if (file) {
      const reader = new FileReader();
      reader.onload = this.handleFileLoad.bind(this);
      reader.readAsDataURL(file);
    }
  }
  handleFileLoad(event: ProgressEvent<FileReader>): void {
    const reader = event.target as FileReader;
    const dataUrl = reader.result as string;

  // Separate the metadata from the Base64 data
    const base64Data = dataUrl.split(',')[1];
    this.avatarUrl.set(base64Data);
    //console.log(this.avatarUrl); // You can use this string to upload the image
  }
}
