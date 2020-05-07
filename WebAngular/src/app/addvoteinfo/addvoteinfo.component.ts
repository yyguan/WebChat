

import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UTIL } from "../share/utilities";
import { HubConnection } from '@aspnet/signalr-client';
import { ResponseData } from '../share/model'
import { map, catchError } from 'rxjs/operators';


//import * as signalR from "@aspnet/signalr";


@Component({
  selector: 'app-add-voteinfo',
  templateUrl: './addvoteinfo.component.html',
  styleUrls: ['./addvoteinfo.component.css']
})
export class AddVoteInfoComponent implements OnInit {
  private _hubConnection: HubConnection;

  public loginUser: LoginEntity = {
    LoginName: "",
    Password: ""
  }
  public addVoteInfo:AddVoteInfo ={
    Id:0,
    Title:"",
    CreateUserId:0,
    CreateUserName:''
  }
  public voteInfos: VoteInfo[];
  loginSuccess: boolean = false;
  nick = '';
  message = '';
  messages: string[] = [];
  errorMessage = '';
  errorAddVoteInfoMessage='';
  passwordErrorMessage = '';
  confirmPassword = '';
  checkResult = false;
  checkPasswordResult = false;
  baseUrl: string = '';
  public addUser: AddUserEntity = {
    UserName: "",
    Email: "",
    LoginName: "",
    MobilePhone: "",
    Password: "",
  }

  constructor(public http: HttpClient) {

  }


  addErrorMessage(message: string) {
    if (this.errorMessage.length > 0)
      this.errorMessage = this.errorMessage + ",";
    this.errorMessage = this.errorMessage + message;
  }
  getVoteInfoList() {
    if(this.nick==''){
      this.errorMessage='请登录后进行操作'
      return;
    }else{
      this.errorMessage='';
    }
    this.baseUrl = "http://localhost:52287/";
    let id = 2;
    var url = this.baseUrl + "api/voteinfo/GetList";
    var result = this.http.get<ResponseData<VoteInfo[]>>(url)
      // .pipe(
      //   map<AddUserEntity>(UTIL.getResponseBody),
      //   catchError(UTIL.handleResponseError)
      // )
      .subscribe(ret => {
        console.log(ret);
        if (ret.responseCode == 0) {
          this.voteInfos = ret.responseData;
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
  checkTitle(event) {
    this.errorAddVoteInfoMessage = "";
    if (this.addVoteInfo.Title == '' || this.addVoteInfo.Title.length < 4) {
      this.checkResult = false;
      if (this.errorAddVoteInfoMessage.indexOf("标题长度") == -1) {
        this.errorAddVoteInfoMessage='标题长度至少为4';
      }
    } else {
      this.errorAddVoteInfoMessage = this.errorAddVoteInfoMessage.replace("标题长度至少为4,", "");
      this.errorAddVoteInfoMessage = this.errorAddVoteInfoMessage.replace("标题长度至少为4", "");
    }
  }
  getToLogin() {
    window.location.href = "/login";
  }
  doCheck2(event) {
    this.checkResult = true;
    this.errorAddVoteInfoMessage = '';
    this.checkTitle(event);
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
    this.checkPassword(event);
  }
  doSubmit(event) {
    console.log(JSON.stringify(this.loginUser));
    this.doCheck(event);
    if (!this.checkResult) {
      return;
    }
    
    this.baseUrl = "http://localhost:52287/";
    var url = this.baseUrl + "api/user/userlogin";
    var result = this.http.post<ResponseData<UserEntity>>(url, this.loginUser, {
      withCredentials: true
    })
      // .pipe(
      //   map<AddUserEntity>(UTIL.getResponseBody),
      //   catchError(UTIL.handleResponseError)
      // )
      .subscribe(ret => {
        if (ret.responseCode == 0) {
          console.log(ret);
          this.nick=ret.responseData.UserName;
          this.errorMessage="登录成功,请获取投票主题";
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



  doSubmit2(event) {
    console.log(JSON.stringify(this.addUser));
    this.doCheck2(event);
    if (!this.checkResult ) {
      return;
    }
    
    this.baseUrl = "http://localhost:52287/";
    var url = this.baseUrl + "api/voteinfo/addvoteinfo";
    var result = this.http.post<ResponseData<VoteInfo>>(url, this.addVoteInfo)
      // .pipe(
      //   map<AddUserEntity>(UTIL.getResponseBody),
      //   catchError(UTIL.handleResponseError)
      // )
      .subscribe(ret => {
        if (ret.responseCode == 0) {
          this.getVoteInfoList();
        } else {
          this.errorMessage = ret.responseMessage;
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

export class VoteInfo {
  Id: number;
  CreateUserId: number;
  CreateUserName: string;
  Title: string;
  CreateDateTime: Date;
  VoteDetails: VoteDetail[]
}

export class AddVoteInfo {
  Id: number;
  CreateUserId: number;
  CreateUserName: string;
  Title: string;
}
export class VoteDetail {
  Id: number;
  VoteInfoId: number;
  ItemTitle: string;
}


export class LoginEntity {
  LoginName: string;
  Password: string;
}