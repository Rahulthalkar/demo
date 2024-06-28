import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  validUser = sessionStorage.getItem('valid-user');
  constructor(private router: Router) {
    if (this.validUser) {
      this.loggedIn.next(true);
    } else {
      sessionStorage.clear();
      this.loggedIn.next(false);
    }
  }
  
  public loggedIn = new BehaviorSubject<boolean>(
    sessionStorage.getItem('valid-user') !== null
  );
  public get isLoggedIn() {
    return this.loggedIn.asObservable();
  }
}
