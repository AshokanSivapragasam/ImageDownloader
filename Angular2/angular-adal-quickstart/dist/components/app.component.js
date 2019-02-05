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
var secret_service_1 = require("../services/secret.service");
var adal_service_1 = require("ng2-adal/dist/services/adal.service");
var router_1 = require("@angular/router");
var AppComponent = /** @class */ (function () {
    function AppComponent(adalService, secretService, router) {
        this.adalService = adalService;
        this.secretService = secretService;
        this.router = router;
        this.adalService.init(this.secretService.adalConfig);
    }
    AppComponent.prototype.ngOnInit = function () {
        this.adalService.handleWindowCallback();
        this.adalService.getUser();
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            template: '<div><router-outlet></router-outlet></div>'
        }),
        __metadata("design:paramtypes", [adal_service_1.AdalService,
            secret_service_1.SecretService,
            router_1.Router])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map