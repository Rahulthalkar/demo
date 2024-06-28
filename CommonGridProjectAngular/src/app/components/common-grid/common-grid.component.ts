import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  ChangeDetectorRef,
  HostListener,
  OnInit,
  NO_ERRORS_SCHEMA,
} from '@angular/core';
import { AppComponent } from '../../app.component';
// import { GridDetailService } from '../../shared/services/griddetail.service';
// import {
//   FilterOperator,
//   GridColumnsDetail,
//   GridHeaderColumn,
//   UpdateColumnDetailRequestModel,
// } from '../../shared/interface/GridDetail';
import cloneDeep from 'lodash/cloneDeep';
import { CdkDragDrop, moveItemInArray, transferArrayItem, DragDropModule } from '@angular/cdk/drag-drop';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NzModalModule } from 'ng-zorro-antd/modal';
//import { TranslateModule } from '@ngx-translate/core';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { FormsModule } from '@angular/forms';
import { NzTableModule } from 'ng-zorro-antd/table';
import { CommonModule, TitleCasePipe } from '@angular/common';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzTagModule } from 'ng-zorro-antd/tag';
//import { NullToDashPipe } from '../../pipes/null-to-dash.pipe';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzI18nModule } from 'ng-zorro-antd/i18n';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { GridDetailsService } from '../../service/grid-details.service';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-common-grid',
  standalone: true,
  imports: [
    NzIconModule,
    NzButtonModule,
    NzInputModule,
    NzPaginationModule,
    CommonModule,
    NzI18nModule,
    DragDropModule,
    NzModalModule,
    TranslateModule,
    NzSelectModule,
    FormsModule,
    NzTableModule,
    TitleCasePipe,
    NzToolTipModule,
    NzTagModule,
   // NullToDashPipe,
    NzGridModule,
    NzCheckboxModule,

  ],
  templateUrl: './common-grid.component.html',
  styleUrl: './common-grid.component.css'
})
export class CommonGridComponent implements OnChanges {
// Input properties
@Input() td: any[] = [];
@Input() th: any[] = [];
@Input() childTd = [];
@Input() childTh = [];
@Input() isViewable: boolean = false;
@Input() isCheckBox: boolean = false;
@Input() isExpand: boolean = false;
@Input() scroll: string = '2200px';
@Input() isPaginations: boolean = false;
@Input() gridName!: string;
@Input() gridId!: number;
@Input() isFilter: boolean = false;
@Input() isEditCols: boolean = false;


// Output events
@Output() selectionChange: EventEmitter<{ count: number; ids: Set<number> }> = new EventEmitter<{
  count: number;
  ids: Set<number>;
}>();
@Output() expandChange: EventEmitter<number> = new EventEmitter<number>();
@Output() filterChanged: EventEmitter<string> = new EventEmitter<string>();
@Output() onClickSetId: EventEmitter<number> = new EventEmitter<number>();
@Output() openModalDetailsChange: EventEmitter<number> = new EventEmitter<number>();


//Global PageSize for
pageSize =5;


expandedRows: Set<number> = new Set<number>();
setOfCheckedId = new Set<number>();
filters: any[] = [];
filteredData: any;
listOfHeaderColumns: any[] = [];
listOfGridColumns: any[] = [];
listOfFilterOperator: any[] = [];
fixedColumns: any[] = [];
visibleColumns: any[] = [];
hiddenColumns: any[] = [];
customColumns: any[] = [];
isFilterVisible: boolean = false;
allChecked: boolean = false;
indeterminate: boolean = false;
isLoading: boolean = true;
isEditCol: boolean = false;
isSmallScreen: boolean = false;
constructor(
  private griddetailService: GridDetailsService,
  private cdr: ChangeDetectorRef,
  private notification: NzNotificationService,
) {
  // defult filter row
  this.filters = [
    {
      columnId: null,
      selectedValue: null,
      inputValue: '',
    },
  ];
}

ngOnChanges(): void {
  if (this.td) {
    // Clone and normalize data for consistent processing
    this.customColumns = this.th;
    this.td = cloneDeep(this.td);
    this.td = this.td.map((dataItem) => {
      const newDataItem: any = {};
      Object.keys(dataItem).forEach((key) => {
        newDataItem[key.toLowerCase()] = dataItem[key];
      });
      return newDataItem;
    });
    this.isLoading = false;
  }


  // Separate visible and hidden columns
  this.fixedColumns = this.customColumns.filter((column) => column.isFix);
  this.visibleColumns = this.customColumns.filter((column) => column.isVisible && !column.isFix);
  this.hiddenColumns = this.customColumns.filter((column) => !column.isVisible && !column.isFix);
}

 // Checks Screen size
 @HostListener('window:resize', ['$event'])
 onResize(event: Event): void {
   this.checkScreenWidth();
 }
 private checkScreenWidth(): void {
   this.isSmallScreen = window.innerWidth <= 1023;
 }


 // Drag-and-drop handler
 drop(event: CdkDragDrop<any[]>): void {
   if (event.previousContainer === event.container) {
     moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
   } else {
     transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
   }
   this.cdr.markForCheck();
 }
 // function to update the column visibility state
 updateColumnVisibility(): void {
   this.visibleColumns = this.visibleColumns.map((column: any) => {
     column.isVisible = true;
     return column;
   });
   this.hiddenColumns = this.hiddenColumns.map((column: any) => {
     column.isVisible = false;
     return column;
   });
 }


 // Edit column: Remove from displayed
 deleteCustom(column: any, index: number): void {
   const removedColumn = this.visibleColumns.splice(index, 1)[0];
   removedColumn.isVisible = false;
   this.hiddenColumns.push(removedColumn);
 }


 // Edit column: Add to displayed
 addCustom(column: any, index: number): void {
   const removedColumn = this.hiddenColumns.splice(index, 1)[0];
   removedColumn.isVisible = true;
   this.visibleColumns.push(removedColumn);
 }


 // Edit columns button click handler
 onHandleEditColumns() {
   this.isEditCol = true;
 }


 // Edit columns: Cancel button click handler
 handleEditCancel() {
   this.cdr.markForCheck();
   this.isEditCol = false;
 }
 // Edit columns: OK button click handler
 handleEditOk() {
   this.updateColumnVisibility();
   this.customColumns = [...this.fixedColumns, ...this.visibleColumns, ...this.hiddenColumns];
   const updatedCols: any= {
     gridColumnDetails: this.customColumns.map((column) => ({ id: column.id, isVisible: column.isVisible })),
   };
   // Save changes to the server
   this.griddetailService.UpdateGridColumnDetails(updatedCols).subscribe(() => {
     // Trigger change detection and update component state
     this.cdr.markForCheck();
     this.isEditCol = false;
   });
 }


 // Add filter column button click handler
 addFilter() {
   this.filters.push({
     columnId: null,
     selectedValue: null,
     inputValue: null,
   });
 }


 // Remove filter button click handler
 removeFilter(index: number) {
   this.filters.splice(index, 1);
 }


 // On column change handler (for filter)
 onColumnChange(selectedColumnId: any, index: any): void {
   this.griddetailService.GetGridColumns(selectedColumnId).subscribe((data: any) => {
     this.listOfGridColumns = data.value as any;
     this.griddetailService.GetOperatorByType(this.listOfGridColumns[0].columnDataType).subscribe((dataNew: any) => {
       this.filters[index].listOfFilterOperator = dataNew.value;


       this.updateAvailableColumns(index);
       this.filters[index].selectedValue = null;
       this.filters[index].inputValue = null;
     });
   });
 }
 updateAvailableColumns(excludedIndex: number): void {
   const selectedColumnIds = this.filters


     .filter((filter, index) => index !== excludedIndex && filter.selectedColumnId)
     .map((filter) => filter.selectedColumnId);
   console.log('selectedColumnIds', selectedColumnIds);
   this.listOfHeaderColumns.forEach((column) => {
     // If the column is selected in any other filter, hide it
     console.log('column ::', column);


     column.isVisible = !selectedColumnIds.includes(column.id);
   });
 }


 // Filter modal: Cancel button click handler
 handleFilterCancel() {
   this.isFilterVisible = false;
 }


 // Filter modal: Open button click handler
 handleFilterOpen() {
   this.th.map((e: any) => (this.gridId = e.gridId));
   this.griddetailService.GetGridHeaderColumnList(this.gridId).subscribe((data: any) => {
     this.listOfHeaderColumns = data.value as any;
   });
   this.isFilterVisible = true;
 }


 ShowNotification(type: string, title: string, details: string): void {
   this.notification.create(type, title, details);
 }


 // Filter modal: OK button click handler


 handleFilterOk() {
   // Check if any filter is incomplete
   const incompleteFilter = this.filters.find(
     (filter) => !filter.columnId || !filter.selectedValue || !filter.inputValue,
   );


   if (incompleteFilter) {
     // If any filter is incomplete, show an error message or handle it accordingly
     console.log('Please complete all filters.');
     // Optionally, you can display an error message to the user


     this.ShowNotification(
       'error',
       '',
       'Please select proper value', // this.translate.instant('InvalidUserOrPassword')
     );
     return; // Stop execution
   }
   // Proceed with filter submission if all filters are complete


   let filtersString = '';
   let invalidFilter = false;
   // Generate filter string
   this.filters.forEach((filter) => {
     const column = this.listOfHeaderColumns.find((column) => column.id === filter.columnId);
     if (!column) {
       return; // Stop execution if column is not found
     }
     const dataType = column.columnDataType;
     const columnName = column.columnName;
     let regex;
     let errorMessage;
     //To validate input
     if (dataType === 'int') {
       if (filter.selectedValue === 'IN' || filter.selectedValue === 'NotIN') {
         regex = /^[0-9,]+$/;
       } else if (filter.selectedValue === 'Between') {
         regex = /^[0-9a-zA-Z\s]+$/;
       } else {
         regex = /^[0-9]+$/;
       }
       errorMessage = 'Please add proper numeric value';
     } else if (dataType === 'datetime') {
       if (filter.selectedValue !== 'Between') {
         regex = /^(?:\d{4}-\d{2}-\d{2}(?:\s\d{2}:\d{2}:\d{2}(?:\.\d{1,6})?)?)?$/;
       }
       errorMessage = 'Please add proper datetime value';
     } else {
       regex = /^[0-9a-zA-Z]+$/;
       errorMessage = 'Please add proper value';
     }
     if (regex) {
       let isValid;
       if (filter.selectedValue === 'Between' || dataType === 'datetime') {
         isValid = regex.test(filter.inputValue);
       } else {
         isValid = regex.test(filter.inputValue) && !filter.inputValue.includes(' ');
       }
       if (!isValid) {
         invalidFilter = true;
         this.ShowNotification('error', '', errorMessage + ' for ' + columnName);
         return; // Stop execution
       }
     }
     this.filters.forEach((filter) => {
       const column = this.listOfHeaderColumns.find((column) => column.id === filter.columnId);
       if (column) {
         const dataType = column.columnDataType;
         const columnName = column.columnName;
         let regex;
         let errorMessage;
         // Rest of your code remains the same
         filtersString += `"${columnName}": {
      "operatorName": "${filter.selectedValue}",
      "dataType": "${dataType}",
      "value": "${filter.inputValue}"
    },\n`;
       }
     });
   });


   if (invalidFilter) {
     return;
   }
   // Trim trailing comma and newline
   filtersString = filtersString.slice(0, -2);


   // Final filter string
   filtersString = `{
    ${filtersString}
  }`;
   this.filteredData = filtersString;
   // Emit filter change event
   this.filterChanged.emit(this.filteredData);
   this.isFilterVisible = false;
 }


 // Toggle row expansion
 toggleExpand(id: number): void {
   if (this.expandedRows.has(id)) {
     this.expandedRows.delete(id);
     this.childTd = [];
   } else {
     this.expandedRows.add(id);
     this.expandChange.emit(id);
   }
 }


 // Check if row is expanded
 isRowExpanded(id: number): boolean {
   return this.expandedRows.has(id);
 }


 // Checkbox: Table Row checked handler
 onRowChecked(id: number, checked: boolean): void {
   this.updateCheckedSet(id, checked);
   this.refreshCheckedStatus();
 }


 // Checkbox: All checked handler
 onAllChecked(value: boolean): void {
   this.td.forEach((row) => this.updateCheckedSet(row.id, value));
   this.refreshCheckedStatus();
 }


 // Refresh checkbox status
 refreshCheckedStatus(): void {
   this.allChecked = this.td.every((row) => this.setOfCheckedId.has(row.id));
   this.indeterminate = this.td.some((row) => this.setOfCheckedId.has(row.id)) && !this.allChecked;


   const selectedCount = this.getSelectedCount();
   const selectedIds = new Set<number>(this.setOfCheckedId);


   // Emit selection change event
   this.selectionChange.emit({ count: selectedCount, ids: selectedIds });
 }


 // Update checked set
 updateCheckedSet(id: number, checked: boolean): void {
   if (checked) {
     this.setOfCheckedId.add(id);
   } else {
     this.setOfCheckedId.delete(id);
   }
 }


 // Get selected Rows count
 getSelectedCount(): number {
   return this.setOfCheckedId.size;
 }
 getColumnKeys(obj: any): string[] {
   return Object.keys(obj);
 }
 // Get selected Rows id
 onClickRow(id: number) {
   this.onClickSetId.emit(id);
 }


 // Modal Details Handler
 openModalDetails(id: number) {
   this.openModalDetailsChange.emit(id);
 }


 // Check whether column in td is Array or not
 isArray(data: any): boolean {
   return Array.isArray(data);
 }


 // Truncate tag names
 truncateItem(item: string, maxLength: number): string {
   if (item.length > maxLength) {
     return item.substr(0, maxLength) + '...';
   }
   return item;
 }
}
