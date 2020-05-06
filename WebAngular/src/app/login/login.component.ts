

import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { UTIL } from "../share/utilities";
import { HubConnection } from '@aspnet/signalr-client';
import { ResponseData } from '../share/model';
import { map, catchError } from 'rxjs/operators';
import { UserEntity } from '../share/model';


//import * as signalR from "@aspnet/signalr";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private _hubConnection: HubConnection;
  connectState: boolean = false;
  nick = '';
  message = '';
  messages: UserMessage[] = [];

  errorMessage = '';
  checkResult = false;
  public loginUser: LoginEntity = {
    LoginName: "",
    Password: ""
  }

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {

    this.baseUrl = "http://localhost:52287/";

  }

  public sendMessage(): void {
    if (this.connectState) {
      console.log(`this.nick=${this.nick}`);
      // alert("连接成功");
      if (this.nick == '') {
        this.errorMessage = "请登录";
        return;
      }
      this._hubConnection
        .invoke('sendToAll', this.nick, this.message)
        .catch(err => console.error(err));

    }else{
       alert("连接失败,不能发送消息,请重新登录");
    }
  }
  public offLine(event){
    if(this.connectState){
      this._hubConnection.stop();
    }
  }
  public messageStyle(name:string){
    let float:string="left";
    if(name==this.nick){
      float='right';
    }
    const style = {
      'float': float,
      'height':'20px',
      'padding-right':'100px'
    };
    return style;
  }

  public styleMethod() {

    let color: string = "green";
    let fontColor: string = "white";
    if (!this.connectState) {
      color = "yellow";
      fontColor = "black";
    }
    const style = {
      'width': '100%',
      'background-color': color,
      'color': fontColor
    };
    return style;
  }

  addErrorMessage(message: string) {
    if (this.errorMessage.length > 0)
      this.errorMessage = this.errorMessage + ",";
    this.errorMessage = this.errorMessage + message;
  }
  checkLoginName(event) {
    this.errorMessage = "";
    if (this.loginUser.LoginName == '' || this.loginUser.LoginName.length < 4) {
      this.checkResult = false;

      if (this.errorMessage.indexOf("登录名长度") == -1) {
        this.addErrorMessage('登录名长度至少为4');
      }
    } else {
      this.errorMessage = this.errorMessage.replace("登录名长度至少为4,", "");
      this.errorMessage = this.errorMessage.replace("登录名长度至少为4", "");
    }
  }
  checkPassword(event) {
    
    this.errorMessage = "";
    if (this.loginUser.Password == '' || this.loginUser.Password.length < 6) {
      this.checkResult = false;

      if (this.errorMessage.indexOf("密码") == -1) {
        this.addErrorMessage('密码至少为6');
      }
    } else {
      this.errorMessage = this.errorMessage.replace("密码至少为6,", "");
      this.errorMessage = this.errorMessage.replace("密码至少为6", "");
    }
  }
  doCheck(event) {
    this.checkResult = true;
    this.errorMessage = '';
    this.checkLoginName(event);
    this.checkPassword(event);
  }
  doSubmit(event) {
    console.log(JSON.stringify(this.loginUser));
    this.doCheck(event);
    if (!this.checkResult) {
      return;
    }
    var url = this.baseUrl + "api/user/userlogin";
    var result = this.http.post<ResponseData<UserEntity>>(url, this.loginUser, {
      withCredentials: true
    })
      // .pipe(
      //   map<AddUserEntity>(UTIL.getResponseBody),
      //   catchError(UTIL.handleResponseError)
      // )
      .subscribe(ret => {
        console.log(ret);
        if (ret.responseCode == 0) {
          this.nick = ret.responseData.userName;
          this.loginUser.LoginName="";
          this.loginUser.Password="";
          this._hubConnection = new HubConnection('http://localhost:5000/chat');
          let _that = this;
          this._hubConnection
            .start()
            .then(() => {
              console.log('Connection started!');
              _that.connectState = true;
            })
            .catch(err => console.log('Error while establishing connection :('));

          this._hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
            console.log(`recive data:nicek=${nick},message=${receivedMessage}`);
            const text = `${nick}: ${receivedMessage}`;
            let userMessage: UserMessage = {
              Name: nick,
              Message: receivedMessage
            };
            this.messages.push(userMessage);
          });
          this._hubConnection.onclose((obj) => {
            _that.connectState = false;
          })

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
        console.log("AddUser fail: " + error);
      });;
    //console.log(JSON.stringify(result));
  }


  ngOnInit() {


  }

}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}


export class LoginEntity {
  LoginName: string;
  Password: string;
}

export class UserMessage {
  Name: string;
  Message: string;
}

