import { Component } from '@angular/core';
import { CommonGridComponent } from '../../components/common-grid/common-grid.component';

@Component({
    selector: 'app-employee',
    standalone: true,
    templateUrl: './employee.component.html',
    styleUrl: './employee.component.css',
    imports: [CommonGridComponent]
})
export class EmployeeComponent {
  th:any[]=[
    {
      "id": 1,
      "gridId": 1,
      "columnName": "id",
      "columnDataType": "int",
      "isVisible": true,
      "isFix": false
  },
  {
    "id": 2,
    "gridId": 1,
    "columnName": "date",
    "columnDataType": "DateTime",
    "isVisible": true,
    "isFix": false
},
{
  "id": 3,
  "gridId": 1,
  "columnName": "username",
  "columnDataType": "string",
  "isVisible": true,
  "isFix": false
},
{
  "id": 4,
  "gridId": 1,
  "columnName": "timein",
  "columnDataType": "DateTime",
  "isVisible": true,
  "isFix": false
},
{
  "id":5 ,
  "gridId": 1,
  "columnName": "timeout",
  "columnDataType": "DateTime",
  "isVisible": true,
  "isFix": false
},

  ];
td:any[]=[
  {
  id:1,
  date:'10/03/2024',
  username:'jay',
  timein:'08:00:00',
  timeout:'17:00:00'
  },
  {
    id:2,
    date:'10/03/2024',
    username:'jay',
    timein:'08:00:00',
    timeout:'17:00:00'
    },
    {
      id:3,
      date:'10/03/2024',
      username:'jay',
      timein:'08:00:00',
      timeout:'17:00:00'
    },
    {
      id:4,
      date:'10/03/2024',
      username:'jay',
      timein:'08:00:00',
      timeout:'17:00:00'
    },
    {
      id:5,
      date:'10/03/2024',
      username:'jay',
      timein:'08:00:00',
      timeout:'17:00:00'
    },
    {
      id:6,
      date:'10/03/2024',
      username:'jay',
      timein:'08:00:00',
      timeout:'17:00:00'
    }
]
}
