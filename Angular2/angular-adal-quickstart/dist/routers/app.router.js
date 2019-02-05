"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var home_component_1 = require("../components/home.component");
var welcome_component_1 = require("../components/welcome.component");
var logged_in_guard_1 = require("../authentication/logged-in.guard");
exports.router = [
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: home_component_1.HomeComponent,
        canActivate: [logged_in_guard_1.LoggedInGuard]
    },
    {
        path: 'welcome',
        component: welcome_component_1.WelcomeComponent
    },
];
exports.routes = router_1.RouterModule.forRoot(exports.router);
//# sourceMappingURL=app.router.js.map