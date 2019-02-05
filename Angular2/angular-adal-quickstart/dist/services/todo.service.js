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
var http_1 = require("@angular/common/http");
var of_1 = require("rxjs/observable/of");
var operators_1 = require("rxjs/operators");
var TodoService = /** @class */ (function () {
    function TodoService(httpClient) {
        this.httpClient = httpClient;
        this.apiRootUri = 'https://ei.microsoft.com/ToDoServiceWithDaemon/api/TodoList';
    }
    /**
     * Handle Http operation that failed.
     * Let the app continue.
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */
    TodoService.prototype.handleError = function (operation, result) {
        if (operation === void 0) { operation = 'operation'; }
        return function (error) {
            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead
            // Let the app keep running by returning an empty result.
            return of_1.of(result);
        };
    };
    TodoService.prototype.getTodoList = function (accessToken) {
        var httpOptions = {
            headers: new http_1.HttpHeaders({ 'Authorization': 'Bearer ' + accessToken + '' })
        };
        return this.httpClient.get(this.apiRootUri, httpOptions)
            .pipe(operators_1.tap(function (todoList) { return console.log("Got " + todoList.length + " items"); }), operators_1.catchError(this.handleError('getTodoList')));
    };
    TodoService.prototype.addTodoList = function (title, accessToken) {
        var httpOptions = {
            headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + accessToken + '' })
        };
        var _body_ = { Title: title };
        return this.httpClient.post(this.apiRootUri, _body_, httpOptions)
            .pipe(operators_1.tap(function (_) { return console.log("Must have been added"); }), operators_1.catchError(this.handleError('addTodoList')));
    };
    TodoService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], TodoService);
    return TodoService;
}());
exports.TodoService = TodoService;
//# sourceMappingURL=todo.service.js.map