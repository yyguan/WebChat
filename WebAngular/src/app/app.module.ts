

// import { BrowserModule } from '@angular/platform-browser';
// import { FormsModule } from '@angular/forms';
// import { NgModule } from '@angular/core';


// import { AppComponent } from './app.component';


// @NgModule({
//   declarations: [
//     AppComponent
//   ],
//   imports: [
//     BrowserModule,
//     FormsModule
//   ],
//   providers: [],
//   bootstrap: [AppComponent]
// })
// export class AppModule { }



import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AddUserComponent } from './adduser/add-user.component';
import { LoginComponent } from './login/login.component';
import { ChatComponent } from './chat/chat.component'
import { AddVoteInfoComponent } from './addvoteinfo/addvoteinfo.component'



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AddUserComponent,
    LoginComponent,
    ChatComponent,
    AddVoteInfoComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpClientJsonpModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'add-user', component: AddUserComponent },
      { path: 'login', component: LoginComponent },
      { path: 'chat', component: ChatComponent },
      { path: 'addvoteinfo', component: AddVoteInfoComponent }
      
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }