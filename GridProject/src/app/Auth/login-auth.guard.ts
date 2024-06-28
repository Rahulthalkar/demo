import {
    ActivatedRouteSnapshot,
    CanActivateFn,
    Router,
    RouterStateSnapshot,
  } from '@angular/router';
  import { Observable, take, map } from 'rxjs';
  import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
  
  @Injectable({
    providedIn: 'root',
  })
  export class LoginAuthGuard {
    constructor(
      private loginAuthService: AuthService,
      private router: Router
    ) {}
    canActivate(
      next: ActivatedRouteSnapshot,
      state: RouterStateSnapshot
    ): Observable<boolean> {
      return this.loginAuthService.isLoggedIn.pipe(
        take(1),
        map((isLoggedIn: boolean) => {
          if (!isLoggedIn) {
            this.router.navigate(['/login']);
            return false;
          }
          return true;
        })
      );
    }
  }
  