export interface GridHeaderColumn {
    id: number;
    gridId: number;
    columnName: string;
    columnDataType: string;
    isSearchable: boolean;
    isVisible: boolean;
    isFix: boolean
}
export interface GridColumnsDetail {
    id: number;
    columnName: string;
    columnDataType: string;
}
export interface FilterOperator {
    operatorId: number;
    dataType: string;
    operatorInWords: string;
    operator: string;
}
export interface FilterData {
    columnName: string;
    operatorName: string;
    dataType: string;
    value: string;
}
export interface GridColumnDetailModel {
    id: number;
    isVisible?: boolean;
}
export interface UpdateColumnDetailRequestModel {
    gridColumnDetails: GridColumnDetailModel[];
}