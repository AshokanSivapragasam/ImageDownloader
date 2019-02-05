"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
// Application specific configuration 
var SecretService = /** @class */ (function () {
    function SecretService() {
    }
    Object.defineProperty(SecretService.prototype, "adalConfig", {
        get: function () {
            return {
                tenant: 'microsoft.onmicrosoft.com',
                clientId: '27c76cba-5610-4191-b08d-c3905aa7eeb9',
                redirectUri: window.location.origin + '/',
                postLogoutRedirectUri: window.location.origin + '/'
            };
        },
        enumerable: true,
        configurable: true
    });
    SecretService = __decorate([
        core_1.Injectable()
    ], SecretService);
    return SecretService;
}());
exports.SecretService = SecretService;
//# sourceMappingURL=secret.service.js.map