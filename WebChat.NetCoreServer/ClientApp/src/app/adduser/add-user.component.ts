

import { Component, Inject ,OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UTIL } from "../share/utilities";

import { map, catchError } from 'rxjs/operators';


 import { HubConnectionBuilder,HubConnection } from '@aspnet/signalr';
//import * as signalR from "@aspnet/signalr";


@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent  implements OnInit {
  //private _hubConnection: HubConnection;
  public forecasts: WeatherForecast[];
  private _hubConnection: HubConnection;
  nick = '';
  message = '';
  messages: string[] = [];
  public addUser: AddUserEntity = {
    UserName: "",
    Email: "",
    LoginName: "",
    MobileNumber: ""
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
  getUserInfo(){
    
    this.baseUrl = "http://localhost:52287/";
    let id=1;
    var url = this.baseUrl + "api/User/"+id.toString();
    var result = this.http.get<UserEntity>(url)
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
        console.log("GetUser fail: " + error);
    });;
  }
  doSubmit() {
    console.log(JSON.stringify(this.addUser));
    var url = this.baseUrl + "api/User/AddUser";
    var result = this.http.post<UserEntity>(url, this.addUser)
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
    //console.log(JSON.stringify(result));
  }

  
  ngOnInit() {
    this.nick = window.prompt('Your name:', 'John');

    this._hubConnection = new HubConnectionBuilder().withUrl("http://localhost:52287/chat").build();
    //new HubConnection('http://localhost:5000/chat');
    this._hubConnection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

      this._hubConnection.on('sendToAll', (nick: string, receivedMessage: string) => {
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


export class AddUserEntity {
  UserName: string;
  LoginName: string;
  MobileNumber: string;
  Email: string;
}

export class UserEntity {
  UserName: string;
  LoginName: string;
  MobileNumber: string;
  Email: string;
  ID:Number
}
