import { Routes } from '@angular/router';
import { EmployeeComponent } from './pages/employee/employee.component';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { LoginComponent } from './pages/login/login.component';
import { MasterGuard } from './Auth/master.guard';
import { RegisterComponent } from './pages/register/register.component';
import { ViewsComponent } from './pages/views/views.component';
import { AutoComplatedTextComponent } from './pages/auto-complated-text/auto-complated-text.component';
import { UpdatePasswordComponent } from './pages/update-password/update-password.component';
import { MyProfileComponent } from './pages/my-profile/my-profile.component';
import { ProfileEditComponent } from './pages/profile-edit/profile-edit.component';
import { CommentsComponent } from './pages/comments/comments.component';
import { UploadImagesComponent } from './pages/upload-images/upload-images.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/employee' },
  { path: 'employee', loadChildren: () => import('./pages/employee/employee-grid.routes').then(m => m.EMPLOYEE_ROUTES),canActivate:[MasterGuard] },
  {
    path:'welcome',
    component:WelcomeComponent,
    canActivate:[MasterGuard]
  }, 
  {
    path:'register',
    component:RegisterComponent
  },
  {
    path:'views',
    component:ViewsComponent,
    canActivate:[MasterGuard]
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'auto',
    component:AutoComplatedTextComponent,
    canActivate:[MasterGuard]
  },
  {
    path:'update-password',
    component:UpdatePasswordComponent,
    canActivate:[MasterGuard]
  },{
    path:'my-profile',
    component:MyProfileComponent,
    canActivate:[MasterGuard]

  } ,{
    path:'edit-profile',
    component:ProfileEditComponent,
    canActivate:[MasterGuard]

  }  
  ,{
    path:'comments',
    component:CommentsComponent
  }  
 ,{
  path:'upload',
  component:UploadImagesComponent
} 
];
