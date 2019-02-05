import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import {AdalService} from 'ng2-adal/dist/core';
import {SecretService} from './services/secret.service';

import { AppComponent }  from './components/app.component';

import {routes} from './routers/app.router'
import {HomeComponent} from "./components/home.component";
import {WelcomeComponent} from "./components/welcome.component";
import {LoggedInGuard} from "./authentication/logged-in.guard";
import { TodoService } from './services/todo.service';


@NgModule({
  imports:      [ BrowserModule, HttpClientModule, routes],
  declarations: [ AppComponent, HomeComponent, WelcomeComponent ],
  providers: [AdalService, SecretService, LoggedInGuard, TodoService],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
