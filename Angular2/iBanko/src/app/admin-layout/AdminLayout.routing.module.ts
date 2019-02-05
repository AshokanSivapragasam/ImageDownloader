import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthguardsService } from './_guards/_authguards.service';
import { HomeComponent } from './home/home.component';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';

const routes: Routes = [
   { path: '', component: HomeComponent, canActivate: [AuthguardsService] },
   { path: 'login', component: LoginComponent },
   { path: 'register', component: RegisterComponent },
   { path: '**', redirectTo: '' }
];

export class AdminLayoutRoutingModule { }
