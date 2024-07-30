import { Component, signal, WritableSignal } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzUploadFile, NzUploadModule, NzUploadXHRArgs } from 'ng-zorro-antd/upload';
import { Observable, Observer, Subscription } from 'rxjs';
import { NzIconModule, NzIconService } from 'ng-zorro-antd/icon';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-upload-images',
  standalone: true,
  imports: [NzUploadModule, NzIconModule],
  templateUrl: './upload-images.component.html',
  styleUrls: ['./upload-images.component.css']
})
export class UploadImagesComponent {
  fileList: WritableSignal<NzUploadFile[]> = signal([]);
  loading: WritableSignal<boolean> = signal(false);
  avatarUrl: WritableSignal<string | undefined> = signal(undefined);
  loginForm: FormGroup;

  constructor(private msg: NzMessageService, private fb: FormBuilder) {
    this.loginForm = fb.group({
      image: [],
    });
  }

  beforeUpload = (file: NzUploadFile, _fileList: NzUploadFile[]): Observable<boolean> =>
    new Observable((observer: Observer<boolean>) => {
      const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png';
      if (!isJpgOrPng) {
        this.msg.error('You can only upload JPG or PNG file!');
        observer.complete();
        return;
      }
      const isLt2M = file.size! / 1024 / 1024 < 2;
      if (!isLt2M) {
        this.msg.error('Image must be smaller than 2MB!');
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

  private base64ToFile(base64: string, filename: string): File {
    const arr = base64.split(',');
    const mime = arr[0].match(/:(.*?);/)![1];
    const bstr = atob(arr[1]);
    let n = bstr.length;
    const u8arr = new Uint8Array(n);
    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, { type: mime });
  }

  handleChange(info: { file: NzUploadFile }): void {
    console.log('handleChange', info);
    switch (info.file.status) {
      case 'uploading':
        this.loading.set(true);
        break;
      case 'done':
        this.getBase64(info.file!.originFileObj!, (img: string) => {
          this.loading.set(false);
          const base64Data = img.split(',')[1];
          this.avatarUrl.set(base64Data);
          this.avatarUrl.set(img);
          console.log('Image uploaded successfully',base64Data);
        });
        break;
      case 'error':
        this.msg.error('Network error');
        this.loading.set(false);
        break;
    }
  }

  customRequest = (item: NzUploadXHRArgs): Subscription => {
    console.log('customRequest', item);
    this.getBase64(item.file as any, (img: string) => {
      this.avatarUrl.set(img);
      const file = this.base64ToFile(img, item.file.name);
      this.loginForm.patchValue({ image: file });
  
      const nzUploadFile: NzUploadFile = {
        uid: item.file.uid,  // Ensure you add the uid from the original file
        name: file.name,
        status: 'done', // Update this status accordingly
        url: this.avatarUrl(),
        originFileObj: file
      };
  
      this.fileList.set([nzUploadFile]);
      console.log('Base64 converted to file', nzUploadFile);
  
      if (item.onSuccess) {
        item.onSuccess({}, nzUploadFile, {});
      }
    });
    return new Subscription();
  };
  

  removeImage(): void {
    this.avatarUrl.set(undefined);
    this.loginForm.patchValue({ image: null });
    this.fileList.set([]);
    console.log('Image removed');
  }

  onsumit():void{
    console.log(this.loginForm.value);
    
  }
}
