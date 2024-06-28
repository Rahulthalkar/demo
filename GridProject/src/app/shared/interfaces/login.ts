export interface Login {
    email: string;
    password: string;
  }
export interface LoginResponse{
    id:number,
    firstName:string,
    lastName:string,
    email:string,
}
export interface APIResult<T> {
    value: T;
    isSuccess: boolean;
    errorMessageKey: string;
    exceptionInfo: string;
}