import { Component, OnInit, signal, WritableSignal } from '@angular/core';
import { CommonGridComponent } from '../../component/common-grid/common-grid.component';
import { GriddetailgridService } from '../../service/griddetailgrid.service';
import { employeeDetails } from '../../shared/interfaces/emplpyeeDetails';
import { FilterOperator, GridHeaderColumn } from '../../shared/interfaces/gridDetail';
import { NzUploadFile, NzUploadModule, NzUploadXHRArgs } from 'ng-zorro-antd/upload';
import { Observable, Observer, Subscription } from 'rxjs';
@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonGridComponent,NzUploadModule],
  templateUrl: './employee.component.html',
  styleUrl: './employee.component.css'
})
export class EmployeeComponent implements OnInit {
//   th:any[]=[
//     {
//       "id": 1,
//       "gridId": 1,
//       "columnName": "id",
//       "columnDataType": "int",
//       "isVisible": false,
//       "isFix": false
//   },
//   {
//     "id": 2,
//     "gridId": 1,
//     "columnName": "date",
//     "columnDataType": "DateTime",
//     "isVisible": true,
//     "isFix": false
// },
// {
//   "id": 3,
//   "gridId": 1,
//   "columnName": "username",
//   "columnDataType": "string",
//   "isVisible": true,
//   "isFix": false
// },
// {
//   "id": 4,
//   "gridId": 1,
//   "columnName": "timein",
//   "columnDataType": "DateTime",
//   "isVisible": true,
//   "isFix": false
// },
// {
//   "id":5 ,
//   "gridId": 1,
//   "columnName": "timeout",
//   "columnDataType": "DateTime",
//   "isVisible": true,
//   "isFix": false
// },

//   ];
// td:any[]=[
//   {
//   id:1,
//   date:'10/03/2024',
//   username:'jay',
//   timein:'08:00:00',
//   timeout:'17:00:00'
//   },
//   {
//     id:2,
//     date:'10/03/2024',
//     username:'jay',
//     timein:'08:00:00',
//     timeout:'17:00:00'
//     },
//     {
//       id:3,
//       date:'10/03/2024',
//       username:'jay',
//       timein:'08:00:00',
//       timeout:'17:00:00'
//     },
//     {
//       id:4,
//       date:'10/03/2024',
//       username:'jay',
//       timein:'08:00:00',
//       timeout:'17:00:00'
//     },
//     {
//       id:5,
//       date:'10/03/2024',
//       username:'jay',
//       timein:'08:00:00',
//       timeout:'17:00:00'
//     },
//     {
//       id:6,
//       date:'10/03/2024',
//       username:'jay',
//       timein:'08:00:00',
//       timeout:'17:00:00'
//     }
// ]
employeeList:employeeDetails[]=[];
employeeHeaderColumn:GridHeaderColumn[]=[];
listOfFilterOperator:FilterOperator[]=[];


constructor(private employeeService:GriddetailgridService){

}
ngOnInit(): void {
//Call the method to get the company list from the service
this.employeeService.GetGridPageDetail('Employees').subscribe((data: any) => {
  const gridDetails = data.value as any[];
  gridDetails.forEach((gridDetail) => {
    const gridId = gridDetail.gridId;
    this.employeeService.GetGridHeaderColumnList(gridId).subscribe((headerData: any) => {
      if (gridId === 1) {
        this.employeeHeaderColumn = headerData.value as GridHeaderColumn[];
      }
    });
  });
});
this.employeeService.GetOperatorByType('').subscribe((data: any) => {
  this.listOfFilterOperator = data.value as FilterOperator[];
});
  this.employeeService.GetEmployeeList().subscribe((data: any) => {
    this.employeeList = data.value as employeeDetails[];
    console.log(this.employeeList);
    
  });
}

}
