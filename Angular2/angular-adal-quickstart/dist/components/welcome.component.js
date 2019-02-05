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
var router_1 = require("@angular/router");
var core_2 = require("ng2-adal/dist/core");
var WelcomeComponent = /** @class */ (function () {
    function WelcomeComponent(router, adalService) {
        this.router = router;
        this.adalService = adalService;
        console.log('Entering welcome');
        if (this.adalService.userInfo.isAuthenticated) {
            this.router.navigate(['/home']);
        }
    }
    WelcomeComponent.prototype.ngOnInit = function () {
        console.log('ngOnInit is called');
    };
    WelcomeComponent.prototype.logIn = function () {
        this.adalService.login();
    };
    WelcomeComponent = __decorate([
        core_1.Component({
            selector: 'welcome',
            template: '<h1>Welcome!</h1><button (click)="logIn()">Login</button>'
        }),
        __metadata("design:paramtypes", [router_1.Router,
            core_2.AdalService])
    ], WelcomeComponent);
    return WelcomeComponent;
}());
exports.WelcomeComponent = WelcomeComponent;
//# sourceMappingURL=welcome.component.js.map