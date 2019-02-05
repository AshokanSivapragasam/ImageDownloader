import {Component} from '@angular/core';
import {AdalService} from 'ng2-adal/dist/core';
import { TodoService } from '../services/todo.service';

@Component({
  selector: 'home',
  template: '<div protected><h1>This is the dashboard page.</h1><button (click)="logOut()">Logout</button><span>AccessToken 001: {{accessToken}}</span><button (click)="getTodoList()">GetToDoList</button></div>'
})
export class HomeComponent {
  public accessToken: string;
  public todoList: any[];
  constructor(
    private adalService: AdalService,
    private todoService: TodoService
  ) {
    console.log('Entering home. Getting access-token');
    this.getAccessToken();
  }

  public getAccessToken() {
    this.adalService.acquireToken('https://microsoft.onmicrosoft.com/TodoListService001').subscribe(
      token => { this.accessToken = token.toString(); },
      error => { console.log(error); }
    );
  }

  public getTodoList() : void {
    this.todoService.getTodoList(this.accessToken).subscribe(
      todoList => {this.todoList = todoList; console.log('Gotten it'); console.log(this.todoList);}
    );
  }

  public addTodoList(title: string) : void {
    this.todoService.addTodoList(title, this.accessToken).subscribe(() => console.log('Added it'));
  }

  public logOut() {
    this.adalService.logOut();
  }
}
