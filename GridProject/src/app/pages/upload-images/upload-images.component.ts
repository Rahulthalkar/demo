import { Component, signal, WritableSignal } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzUploadFile, NzUploadModule, NzUploadXHRArgs } from 'ng-zorro-antd/upload';
import { Observable, Observer, Subscription } from 'rxjs';
import { NzIconModule, NzIconService } from 'ng-zorro-antd/icon';
import { FormBuilder, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-upload-images',
  standalone: true,
  imports: [NzUploadModule,NzIconModule],
  templateUrl: './upload-images.component.html',
  styleUrl: './upload-images.component.css'
})
export class UploadImagesComponent {

  fileList: WritableSignal<NzUploadFile[]> = signal([]);
  loading :WritableSignal<boolean>=signal(false)
  avatarUrl: WritableSignal<string | undefined> = signal(undefined);
  loginForm:FormGroup;

  constructor(private msg: NzMessageService,private fb:FormBuilder) {
    this.loginForm=fb.group({
      image:[],
    })
  }

  beforeUpload = (file: NzUploadFile, _fileList: NzUploadFile[]): Observable<boolean> =>
    new Observable((observer: Observer<boolean>) => {
      const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
      if (!isJpgOrPng) {
        this.msg.error('You can only upload JPG file!');
        observer.complete();
        return;
      }
      const isLt2M = file.size! / 1024 / 1024 < 2;
      if (!isLt2M) {
        this.msg.error('Image must smaller than 2MB!');
        observer.complete();
        return;
      }
      observer.next(isJpgOrPng && isLt2M);
      observer.complete();
    });

  private getBase64(img: File, callback: (img: string) => void): void {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result!.toString()));
    reader.readAsDataURL(img);
  }

  handleChange(info: { file: NzUploadFile }): void {
    switch (info.file.status) {
      case 'uploading':
        this.loading.set(true);
        break;
      case 'done':
        // Get this url from response in real world.
        this.getBase64(info.file!.originFileObj!, (img: string) => {
          this.loading.set(false);
          this.avatarUrl.set(img);
          console.log(this.avatarUrl.set(img));
          
        });
        break;
      case 'error':
        this.msg.error('Network error');
        this.loading.set(false);
        break;
    }
  }
  customRequest = (item: NzUploadXHRArgs): Subscription => {
      this.getBase64(item.file as any, (img: string) => {
        this.avatarUrl.set(img)
        console.log(this.avatarUrl.set(img));
        
        this.loginForm.patchValue({ Photo: item.file });
       // console.log('ddddd=====',this.loginForm.patchValue({ Photo: item.file }));
        
        this.fileList.set([item.file as NzUploadFile]);
        if (item.onSuccess) {
          item.onSuccess({}, item.file!, {});
        }
      });
      return new Subscription();
    };
  removeImage(): void {
      this.avatarUrl.set(undefined)
      this.loginForm.patchValue({ Photo: null });
      this.fileList.set([])
    }
}
