"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var core_2 = require("ng2-adal/dist/core");
var todo_service_1 = require("../services/todo.service");
var HomeComponent = /** @class */ (function () {
    function HomeComponent(adalService, todoService) {
        this.adalService = adalService;
        this.todoService = todoService;
        console.log('Entering home. Getting access-token');
        this.getAccessToken();
    }
    HomeComponent.prototype.getAccessToken = function () {
        var _this = this;
        this.adalService.acquireToken('https://microsoft.onmicrosoft.com/TodoListService001').subscribe(function (token) { _this.accessToken = token.toString(); }, function (error) { console.log(error); });
    };
    HomeComponent.prototype.getTodoList = function () {
        var _this = this;
        this.todoService.getTodoList(this.accessToken).subscribe(function (todoList) { _this.todoList = todoList; console.log('Gotten it'); console.log(_this.todoList); });
    };
    HomeComponent.prototype.addTodoList = function (title) {
        this.todoService.addTodoList(title, this.accessToken).subscribe(function () { return console.log('Added it'); });
    };
    HomeComponent.prototype.logOut = function () {
        this.adalService.logOut();
    };
    HomeComponent = __decorate([
        core_1.Component({
            selector: 'home',
            template: '<div protected><h1>This is the dashboard page.</h1><button (click)="logOut()">Logout</button><span>AccessToken 001: {{accessToken}}</span><button (click)="getTodoList()">GetToDoList</button></div>'
        }),
        __metadata("design:paramtypes", [core_2.AdalService,
            todo_service_1.TodoService])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map