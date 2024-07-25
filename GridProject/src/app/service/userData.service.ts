import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { APIResult, Login, LoginResponse } from '../shared/interfaces/login';
import { LoginAuthGuard } from '../Auth/login-auth.guard';
import { AuthService } from '../Auth/auth.service';
import { map, Observable, pipe } from 'rxjs';
@Injectable({
    providedIn: 'root',
  })
export class UserService {
    private urlFor = environment.baseUrl;
    successUser: any;
    userId: any;
    constructor(
      private http: HttpClient,
      private notification: NzNotificationService,
      private loginAuthService:AuthService,
      private router: Router,
    ) { }
  
    private getHttpOptions() {
      return {
        headers: new HttpHeaders({
          'Content-Type': 'application/json; charset=utf-8',
          'Access-Control-Allow-Origin': '*',
          'Access-Control-Allow-Credentials': 'true',
          'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
          'Access-Control-Allow-Headers':
            'Origin, Content-Type, Accept, X-Custom-Header, Upgrade-Insecure-Requests',
        }),
        withCredentials: true,
      };
    }
    isChangePassword: any
  
    login(login: Login) {
      this.http
        .post<APIResult<LoginResponse>>(
          environment.baseUrl + 'api/Employee/Login',
          login
        )
        .subscribe({
          next: (responseOfLogin: APIResult<LoginResponse>) => {
            if (responseOfLogin.isSuccess) {
              const user = responseOfLogin.value;
              this.successUser = user;
              this.userId = user.id;
              sessionStorage.setItem('userId', this.userId);
              sessionStorage.setItem('userEmail', user.email);
              sessionStorage.setItem('valid-user', JSON.stringify(this.successUser));
              this.router.navigate(['/welcome'])
               this.loginAuthService.loggedIn.next(true);
            }else{
               this.ShowNotification(
                    'error',
                    '',
                    'Invalid email id or password.'
                );
            }
          },
          error: (err) => {
            if (err.error.errorMessageKey == 'InvalidUserOrPassword') {
              this.ShowNotification(
                'error',
                '',
                'Invalid email id or password.'
              );
            } else {
              this.ShowNotification('error', '', 'API error');
            }
          },
        });
    }
  
    //call IsOTPValid API Service here
  
    // IsOTPValid(id: number, otp: number): Observable<any> {
    //   const httpOptionsToUse = this.getHttpOptions();
    //   const url = `${environment.baseUrl}api/User/IsOTPValid?userId=${id}&oTP=${otp}`;
  
    //   return this.http.post(url, httpOptionsToUse).pipe((response: any) => {
    //     if (response) {
    //       sessionStorage.setItem('valid-user', JSON.stringify(this.successUser));
    //     }
    //     return response;
    //   });
    // }
  
  
    logout() {
      // Deleting Cookie
      let user = sessionStorage.getItem('valid-user');
      sessionStorage.clear();
      this.loginAuthService.loggedIn.next(false);
      if (user) {
        sessionStorage.clear();
        this.router.navigate(['/login']);
      }
    }

    registerUser(userData: any): Observable<any> {
      var httpOptionsToUse = this.getHttpOptions();
      return this.http
        .post(this.urlFor + `api/Employee/CreateUser`, userData, httpOptionsToUse)
        .pipe((response: any) => {
          return response;
        });
    }

    SearchEmployee(data:any):Observable<any>{
      var httpOptionsToUse=this.getHttpOptions();
      return this.http.post(this.urlFor+`api/Employee/SearchEmployee?searchCriteria=${data}`,httpOptionsToUse)
      pipe((response:any)=>{
        return response;
      })
    }

    UpdatePassword(formData: any): Observable<any> {
      var httpOptionsToUse = this.getHttpOptions();
      return this.http
          .post(this.urlFor + 'api/Employee/UpdatePassword', formData, httpOptionsToUse)
          .pipe((response: any) => {
              return response;
          });
  }
  ValidateUser(emailId:string,password:string): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();

    const url = `${this.urlFor}api/Employee/ValidateUser?email=${emailId}&password=${password}`;
    return this.http
        .get(url, httpOptionsToUse)
        .pipe((response: any) => {
            return response;
   });  
  }
  UpdateEmployees(formData: any): Observable<any> {
    var httpOptionsToUse = this.getHttpOptions();
    return this.http
        .post(this.urlFor + 'api/Employee/UpdateEmployees', formData, httpOptionsToUse)
        .pipe((response: any) => {
            return response;
        });
}
  GetUserDetailById(userId:number): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();

    const url = `${this.urlFor}api/Employee/GetUserDetailById?userId=${userId}`;
    return this.http
        .post(url, httpOptionsToUse)
        .pipe((response: any) => {
            return response;
   });  
  }


  Comments(userData: any): Observable<any> {
    var httpOptionsToUse = this.getHttpOptions();
    return this.http
      .post(this.urlFor + `api/Comments/Comments`, userData, httpOptionsToUse)
      .pipe((response: any) => {
        return response;
      });
  }
  ReplayComments(userData: any): Observable<any> {
    var httpOptionsToUse = this.getHttpOptions();
    return this.http
      .post(this.urlFor + `api/Comments/ReplayComments`, userData, httpOptionsToUse)
      .pipe((response: any) => {
        return response;
      });
  }
  GetCommentById(userId:number): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();

    const url = `${this.urlFor}api/Comments/GetCommentById?userId=${userId}`;
    return this.http
        .post(url, httpOptionsToUse)
        .pipe((response: any) => {
            return response;
   });  
  }

  GetCommentByCommentId(userId:number,replayId:number): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();

    const url = `${this.urlFor}api/Comments/GetCommentByCommentId?userId=${userId}&commentId=${replayId}`;
    return this.http
        .post(url, httpOptionsToUse)
        .pipe((response: any) => {
            return response;
   });  
  }
    ShowNotification(type: string, title: string, details: string): void {
      this.notification.create(type, title, details);
    }
  }