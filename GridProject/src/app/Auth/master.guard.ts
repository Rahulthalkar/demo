import {
    ActivatedRouteSnapshot,
    RouterStateSnapshot,
    UrlTree,
  } from '@angular/router';
  import { Observable } from 'rxjs';
  import { Injectable } from '@angular/core';
  import { LoginAuthGuard } from './login-auth.guard';
  @Injectable({
    providedIn: 'root',
  })
  export class MasterGuard {
    constructor(
      private loginAuthGuard: LoginAuthGuard,
    ) {}
    canActivate(
      next: ActivatedRouteSnapshot,
      state: RouterStateSnapshot
    ): Observable<boolean | UrlTree> {
      return (
        this.loginAuthGuard.canActivate(next, state) 
      );
    }
  }
  