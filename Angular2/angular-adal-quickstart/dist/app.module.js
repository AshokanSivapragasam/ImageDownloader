"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var http_1 = require("@angular/common/http");
var core_2 = require("ng2-adal/dist/core");
var secret_service_1 = require("./services/secret.service");
var app_component_1 = require("./components/app.component");
var app_router_1 = require("./routers/app.router");
var home_component_1 = require("./components/home.component");
var welcome_component_1 = require("./components/welcome.component");
var logged_in_guard_1 = require("./authentication/logged-in.guard");
var todo_service_1 = require("./services/todo.service");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [platform_browser_1.BrowserModule, http_1.HttpClientModule, app_router_1.routes],
            declarations: [app_component_1.AppComponent, home_component_1.HomeComponent, welcome_component_1.WelcomeComponent],
            providers: [core_2.AdalService, secret_service_1.SecretService, logged_in_guard_1.LoggedInGuard, todo_service_1.TodoService],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map