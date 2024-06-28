import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
// import { environment } from 'src/environments/environment';
import { ActivatedRoute, Router } from '@angular/router';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { TranslateService } from '@ngx-translate/core';


@Injectable({
  providedIn: 'root'
})
export class GridDetailsService {

  private urlFor = 'localhost:4200';


  GetGridHeaderColumnList(gridId: any): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();


    // Assuming userId is part of the URL (adjust accordingly if it's a query parameter)
    const url = `${this.urlFor}api/GridSearch/GetGridDetail?gridId=${gridId}`;


    return this.http.get(url, httpOptionsToUse).pipe((response: any) => {
      return response;
    });
  }
  GetGridPageDetail(pageName: any): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();
    const url = `${this.urlFor}api/GridSearch/GetGridPageDetail?pageName=${pageName}`;


    return this.http.get(url, httpOptionsToUse).pipe((response: any) => {
      return response;
    });
  }


  GetGridColumns(columnId: number): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();


    // Assuming userId is part of the URL (adjust accordingly if it's a query parameter)
    const url = `${this.urlFor}api/GridSearch/GetGridColumns?columnId=${columnId}`;


    return this.http.get(url, httpOptionsToUse).pipe((response: any) => {
      return response;
    });
  }


  GetOperatorByType(operatorType: string): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();


    // Assuming userId is part of the URL (adjust accordingly if it's a query parameter)
    const url = `${this.urlFor}api/GridSearch/GetOperatorByType?operatorType=${operatorType}`;


    return this.http.get(url, httpOptionsToUse).pipe((response: any) => {
      return response;
    });
  }


  UpdateGridColumnDetails(requestModel: any): Observable<any> {
    const httpOptionsToUse = this.getHttpOptions();


    // Assuming userId is part of the URL (adjust accordingly if it's a query parameter)
    const url = `${this.urlFor}api/GridSearch/UpdateGridColumnDetails`;
    return this.http.post(url, requestModel, httpOptionsToUse);
  }


  constructor(
    private http: HttpClient,
    private router: Router,
    public translate: TranslateService,
    private notification: NzNotificationService,
    private route: ActivatedRoute,
  ) {}
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };


  private getHttpOptions() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Credentials': 'true',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, Accept, X-Custom-Header, Upgrade-Insecure-Requests',
        // 'Authorization': `Bearer ${sessionStorage.getItem('token')}`
      }),
      withCredentials: true,
    };
  }


  private getHttpOptionsForFormData() {
    return {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Credentials': 'true',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Origin, Content-Type, Accept, X-Custom-Header, Upgrade-Insecure-Requests',
        //'Authorization': `Bearer ${sessionStorage.getItem('token')}`
      }),
      withCredentials: true,
    };
  }


  ShowNotification(type: string, title: string, details: string): void {
    this.notification.create(type, title, details);
  }


  handleErrorFromAngularGlobal(error: any) {
    if (error.status === 401 || error.status === 404) {
      // Handle Unauthorized (401) response and the issue of 404 not found. Because if the cookie expires, it returns the message saying 404 not found.
      this.ShowNotification('error', '', this.translate.instant('SessionTimedOut'));
      this.router.navigate(['/login']);
    }
    if (error.status == undefined) {
      sessionStorage.clear();
      this.ShowNotification('error', '', this.translate.instant('Unauthorized'));
      this.router.navigate(['/login']);
      // window.location.href = environment.redirectUrl;
    }
  }

}
