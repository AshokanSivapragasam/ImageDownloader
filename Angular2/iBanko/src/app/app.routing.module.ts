import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';

const routes: Routes = [
   { path: '', component: AdminLayoutComponent, children: [
     {path: '', loadChildren: './admin-layout/AdminLayout.routing.module#AdminLayoutRoutingModule'}] }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AdminLayoutRoutingModule { }
