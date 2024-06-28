import { Routes } from '@angular/router';
import { EmployeeComponent } from './pages/employee/employee.component';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { LoginComponent } from './pages/login/login.component';
import { MasterGuard } from './Auth/master.guard';
import { RegisterComponent } from './pages/register/register.component';
import { ViewsComponent } from './pages/views/views.component';
import { AutoComplatedTextComponent } from './pages/auto-complated-text/auto-complated-text.component';
import { UpdatePasswordComponent } from './pages/update-password/update-password.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/employee' },
  { path: 'employee', loadChildren: () => import('./pages/employee/employee-grid.routes').then(m => m.EMPLOYEE_ROUTES),canActivate:[MasterGuard] },
  {
    path:'welcome',
    component:WelcomeComponent,
  }, 
  {
    path:'register',
    component:RegisterComponent
  },
  {
    path:'views',
    component:ViewsComponent
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'auto',
    component:AutoComplatedTextComponent
  },
  {
    path:'update-password',
    component:UpdatePasswordComponent
  }  
 
];
