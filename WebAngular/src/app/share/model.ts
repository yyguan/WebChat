
export class ResponseData<T> {
    responseCode: number;
    responseMessage: string;
    responseData: T;
}


export class UserEntity {
    userName: string;
    loginName: string;
    mobilePhone: string;
    email: string;
    id: Number
  }
  export class BaseUrlConfig{
      static url:string="http://localhost:54961/";
  }