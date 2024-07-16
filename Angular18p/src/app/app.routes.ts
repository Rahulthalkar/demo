import { Routes } from '@angular/router';
import { StudentComponent } from './pages/student/student.component';
import { AddEditComponent } from './pages/add-edit/add-edit.component';
import { AddStudComponent } from './pages/add-stud/add-stud.component';
import { ProfileComponent } from './pages/profile/profile.component';

export const routes: Routes = [
    {path:'',component:StudentComponent},
    {
        path:'edit',component:AddEditComponent
    },
    {
        path:'add',component:AddStudComponent
    },
    {
        path:'profile',component:ProfileComponent
    }
];
