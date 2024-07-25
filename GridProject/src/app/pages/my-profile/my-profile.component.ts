import { Component, OnInit, signal } from '@angular/core';
import { UserService } from '../../service/userData.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-my-profile',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.css'
})
export class MyProfileComponent implements OnInit {
  fullName=signal('');
  email=signal('');
  userViewModel:any;
  constructor(private userService:UserService){
  }
  ngOnInit(): void {
    const user= sessionStorage.getItem('valid-user');
    if(user){
       const userId=JSON.parse(user)
      this.userService.GetUserDetailById(userId.id).subscribe({
        next:(response:any)=>{
          if(response.isSuccess){
            this.userViewModel=response.value;
            this.fullName.set(`${response.value.firstName} ${response.value.lastName} `);
            this.email.set(`${response.value.email}`);
          }
        }
      })
    }       
    
  }
}
