

import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpClientJsonpModule, HttpHeaders } from '@angular/common/http';
import { UTIL } from "../share/utilities";
import { HubConnection } from '@aspnet/signalr-client';
import { ResponseData } from '../share/model';
import { map, catchError } from 'rxjs/operators';
import { UserEntity,BaseUrlConfig } from '../share/model';


//import * as signalR from "@aspnet/signalr";


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  private _hubConnection: HubConnection;

  nick = '';
  message = '';
  messages: string[] = [];
  errorMessage = '';
  checkResult = false;
  public loginUser: LoginEntity = {
    LoginName: "",
    Password: ""
  }

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {

    this.baseUrl = BaseUrlConfig.url;

  }


  public sendMessage(): void {
    this._hubConnection
      .invoke('sendToAll', this.nick, this.message)
      .catch(err => console.error(err));
  }

  ngOnInit() {
    //this.nick = window.prompt('Your name:', 'John');
    
    this.baseUrl = BaseUrlConfig.url;
    var url = this.baseUrl + "api/user/GetCurrentUser";
    var result = this.http.get<ResponseData<UserEntity>>(url )
      // .pipe(
      //   map<AddUserEntity>(UTIL.getResponseBody),
      //   catchError(UTIL.handleResponseError)
      // )
      .subscribe(ret => {
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


    this._hubConnection = new HubConnection('http://localhost:5000/chat');

    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
      console.log(`recive data:nicek=${nick},message=${receivedMessage}`);
      const text = `${nick}: ${receivedMessage}`;
      this.messages.push(text);
    });


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

