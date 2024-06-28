import { Routes } from '@angular/router';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { LoginComponent } from './pages/login/login.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/employee' },
  { path: 'employee', loadChildren: () => import('./pages/employee/employee.routers').then(m => m.EMPLOYEE_ROUTES) },
  {
    path:'welcome',
    component:WelcomeComponent,
    title:'Welcome'
  },
  {
    path:'login',
    component:LoginComponent,
    title:'login'
  }
];
