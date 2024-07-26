import { Component, OnInit } from '@angular/core';
import { employeeDetails } from '../../shared/interfaces/emplpyeeDetails';
import { GriddetailgridService } from '../../service/griddetailgrid.service';
import { NzCardModule } from 'ng-zorro-antd/card';
import {  NzTableModule} from 'ng-zorro-antd/table';
import { da_DK } from 'ng-zorro-antd/i18n';
import { NzInputModule } from 'ng-zorro-antd/input';
import { UserService } from '../../service/userData.service';
import { FormsModule } from '@angular/forms';
interface DataItem {
  firstName: string;
  lastName: string;
  userName: string;
  phone: string;
}
@Component({
  selector: 'app-welcome',
  standalone: true,
  imports:[NzCardModule,NzTableModule,NzInputModule,FormsModule],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {
  employeeList:employeeDetails[]=[];
  //sittingArrangementsDetail = signal(null)
  userProfile: any = {};
  searchCriteria: string = '';
 // imgPrefix = 'data:image/png;base64,'
  imgPrefix= 'data:image/png;base64,';
  listOfColumn = [
    {
      title: 'First Name',
      compare: (a: DataItem, b: DataItem) => a.firstName.localeCompare(b.firstName),
      priority: false
    },
    {
      title: 'Last Name',
      compare: (a: DataItem, b: DataItem) => a.lastName.localeCompare(b.lastName),
      priority: 3
    },
    {
      title: 'User Name',
      compare: (a: DataItem, b: DataItem) =>  a.userName.localeCompare(b.userName),
      priority: 2
    },
    {
      title: 'Phone no',
      compare: (a: DataItem, b: DataItem) =>  a.phone.localeCompare(b.phone),

      priority: 1
    },
    {
      title: 'Photo',
      compare: (a: DataItem, b: DataItem) =>  a.phone.localeCompare(b.phone),

      priority: 1
    }
  ];
  constructor(private employeeService:GriddetailgridService,
    private userService:UserService
  ) { }

  ngOnInit() { 
    const userId = 1;//JSON.parse(sessionStorage.getItem('valid-user') || '{}').id;
    // this.employeeService.GetEmployeeList().subscribe((data: any) => {
    //   this.employeeList = data.value as employeeDetails[];
    // });
    this.GetEmployeeList();
    this.employeeService.GetUserProfile(userId).subscribe((response:any)=>{
     this.userProfile=response.value;
    });

  }

  GetEmployeeList(){
    this.employeeService.GetEmployeeList().subscribe((data: any) => {
      this.employeeList = data.value as employeeDetails[];
    });
  }
  searchEmployee(){
    if(this.searchCriteria.trim()===''){
      this.GetEmployeeList();
    }
    this.userService.SearchEmployee(this.searchCriteria).subscribe((response:any)=>{
      this.employeeList=response.value as employeeDetails[]
    })
  }
    
  
}
