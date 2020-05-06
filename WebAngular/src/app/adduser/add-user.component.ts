

import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UTIL } from "../share/utilities";
import { HubConnection } from '@aspnet/signalr-client';
import { ResponseData } from '../share/model'
import { map, catchError } from 'rxjs/operators';


//import * as signalR from "@aspnet/signalr";


@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {
  private _hubConnection: HubConnection;
  public forecasts: WeatherForecast[];
  loginSuccess: boolean = false;
  nick = '';
  message = '';
  messages: string[] = [];
  errorMessage = '';
  passwordErrorMessage = '';
  confirmPassword = '';
  checkResult = false;
  checkPasswordResult = false;
  public addUser: AddUserEntity = {
    UserName: "",
    Email: "",
    LoginName: "",
    MobilePhone: "",
    Password: "",
  }

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {

    this.baseUrl = "http://localhost:52287/";
    http.get<WeatherForecast[]>(this.baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }


  public sendMessage(): void {
    this._hubConnection
      .invoke('sendToAll', this.nick, this.message)
      .catch(err => console.error(err));
  }
  //修改注册成功状态,默认修改为false
  changeLoginSuccess(result:boolean=false){
    this.loginSuccess=result;
  }
  checkPassword(event) {
    this.changeLoginSuccess();
    this.checkPasswordResult = true;
    this.passwordErrorMessage = '';
    //alert("111");
    if (this.addUser.Password.length < 6) {
      this.checkPasswordResult = false;
      if (this.passwordErrorMessage.indexOf("密码长度") == -1) {
        if (this.passwordErrorMessage.length > 0)
          this.passwordErrorMessage = this.passwordErrorMessage + ",";
        this.addPasswordErrorMessage('密码长度至少为6');
      }
      return;
    } else {
      this.passwordErrorMessage = this.passwordErrorMessage.replace("密码长度至少为6,", "");
      this.passwordErrorMessage = this.passwordErrorMessage.replace("密码长度至少为6", "");
    }
    if (this.confirmPassword != this.addUser.Password) {
      this.checkPasswordResult = false;

      if (this.passwordErrorMessage.indexOf("2次输入的密码不一致") == -1) {
        this.addPasswordErrorMessage('2次输入的密码不一致');
      }
    }
    else {
      this.passwordErrorMessage = this.passwordErrorMessage.replace("2次输入的密码不一致,", "");
      this.passwordErrorMessage = this.passwordErrorMessage.replace("2次输入的密码不一致", "");
    }
  }
  addPasswordErrorMessage(message: string) {
    if (this.passwordErrorMessage.length > 0)
      this.passwordErrorMessage = this.passwordErrorMessage + ",";
    this.passwordErrorMessage = this.passwordErrorMessage + message;
  }
  addErrorMessage(message: string) {
    if (this.errorMessage.length > 0)
      this.errorMessage = this.errorMessage + ",";
    this.errorMessage = this.errorMessage + message;
  }
  getUserInfo() {

    this.baseUrl = "http://localhost:52287/";
    let id = 2;
    var url = this.baseUrl + "api/User/" + id.toString();
    var result = this.http.get<ResponseData<UserEntity>>(url)
      // .pipe(
      //   map<AddUserEntity>(UTIL.getResponseBody),
      //   catchError(UTIL.handleResponseError)
      // )
      .subscribe(ret => {
        console.log(ret);
        if (ret.responseCode == 0) {
          console.log(`ret.ResponseData=${ret.responseData}`);
        } else {
          this.errorMessage = ret.responseMessage;
        }
        // if (ret.isSuccess) {
        //    console.log(JSON.stringify( ret.data));
        // } else {
        //   //this._svc_msg.log("GetRisk fail: " + ret.errorMessage);
        //   console.log("AddUser fail: " + ret.errorMessage);
        // }
      }, (error) => {
        //this.isLoading = false;
        //this._svc_msg.log("GetRisk fail: " + error);
        console.log("GetUser fail: " + error);
      });;
  }
  checkName(event) {
    this.changeLoginSuccess();
    this.errorMessage = "";
    if (this.addUser.UserName == '' || this.addUser.UserName.length < 2) {
      this.checkResult = false;
      if (this.errorMessage.indexOf("昵称长度") == -1) {
        this.addErrorMessage('昵称长度至少为2');
      }
    } else {
      this.errorMessage = this.errorMessage.replace("昵称长度至少为2,", "");
      this.errorMessage = this.errorMessage.replace("昵称长度至少为2", "");
    }
  }
  checkEmail(event) {
    this.changeLoginSuccess();
    this.errorMessage = "";
    this.checkName(event);
    if (this.addUser.Email == '' || this.addUser.Email.length < 8) {
      this.checkResult = false;
      if (this.errorMessage.indexOf("邮箱") == -1) {
        this.addErrorMessage('邮箱长度至少为8');
      }
    } else {
      this.errorMessage = this.errorMessage.replace("邮箱长度至少为8,", "");
      this.errorMessage = this.errorMessage.replace("邮箱长度至少为8", "");
    }
  }
  checkLoginName(event) {
    this.changeLoginSuccess();
    this.errorMessage = "";
    this.checkName(event);
    this.checkEmail(event);
    if (this.addUser.LoginName == '' || this.addUser.LoginName.length < 4) {
      this.checkResult = false;

      if (this.errorMessage.indexOf("登录名长度") == -1) {
        this.addErrorMessage('登录名长度至少为4');
      }
    } else {
      this.errorMessage = this.errorMessage.replace("登录名长度至少为4,", "");
      this.errorMessage = this.errorMessage.replace("登录名长度至少为4", "");
    }
  }
  checkMobilePhone(event) {
    this.changeLoginSuccess();
    this.checkName(event);
    this.checkEmail(event);
    this.checkLoginName(event);

    if (this.addUser.MobilePhone == '' || this.addUser.MobilePhone.length < 11) {
      this.checkResult = false;

      if (this.errorMessage.indexOf("手机号码度") == -1) {
        this.addErrorMessage('手机号码至少为11');
      }
    } else {
      console.log(`手机号码验证通过进行替换,errorMessage=${this.errorMessage}`);
      this.errorMessage = this.errorMessage.replace("手机号码至少为11,", "");
      this.errorMessage = this.errorMessage.replace("手机号码至少为11", "");
    }
  }
  getToLogin(){
    window.location.href="/login";
  }
  doCheck(event) {
    this.checkResult = true;
    this.errorMessage = '';
    this.checkName(event);
    this.checkEmail(event);
    this.checkLoginName(event);
    this.checkMobilePhone(event);
  }
  doSubmit(event) {
    console.log(JSON.stringify(this.addUser));
    this.doCheck(event);
    if (!this.checkResult || !this.checkPasswordResult) {
      return;
    }
    var url = this.baseUrl + "api/User/AddUser";
    var result = this.http.post<ResponseData<UserEntity>>(url, this.addUser)
      // .pipe(
      //   map<AddUserEntity>(UTIL.getResponseBody),
      //   catchError(UTIL.handleResponseError)
      // )
      .subscribe(ret => {
        if (ret.responseCode == 0) {
          this.changeLoginSuccess(true);
        }else{
          this.errorMessage=ret.responseMessage;
        }
        console.log(ret);
        // if (ret.isSuccess) {
        //    console.log(JSON.stringify( ret.data));
        // } else {
        //   //this._svc_msg.log("GetRisk fail: " + ret.errorMessage);
        //   console.log("AddUser fail: " + ret.errorMessage);
        // }
      }, (error) => {
        //this.isLoading = false;
        //this._svc_msg.log("GetRisk fail: " + error);
        console.log("AddUser fail: " + error);
      });;
    //console.log(JSON.stringify(result));
  }


  ngOnInit() {
    // this.nick = window.prompt('Your name:', 'John');

    // this._hubConnection = new HubConnection('http://localhost:5000/chat');

    // this._hubConnection
    //   .start()
    //   .then(() => console.log('Connection started!'))
    //   .catch(err => console.log('Error while establishing connection :('));

    // this._hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
    //   console.log(`recive data:nicek=${nick},message=${receivedMessage}`);
    //   const text = `${nick}: ${receivedMessage}`;
    //   this.messages.push(text);
    // });

  }

}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}


export class AddUserEntity {
  UserName: string;
  LoginName: string;
  MobilePhone: string;
  Email: string;
  Password: string;
}

export class UserEntity {
  UserName: string;
  LoginName: string;
  MobilePhone: string;
  Email: string;
  ID: Number
}
