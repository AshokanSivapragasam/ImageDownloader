import { NgModule } from '@angular/core';
import { RouterModule, Routes, Route } from '@angular/router';

import { HomeComponent } from './components/home/home.component';
import { AuthGuardService } from './services/auth/authguard.service';
import { LoginComponent } from './components/login/login.component';
import { CreateStudentComponent } from './components/student/create-student/create-student.component';
import { ListStudentsComponent } from './components/student/list-students/list-students.component';
import { UpdateStudentComponent } from './components/student/update-student/update-student.component';
import { DeleteStudentComponent } from './components/student/delete-student/delete-student.component';
import { GetStudentComponent } from './components/student/get-student/get-student.component';

const childRoutes: Routes = [
  { path: '', component: ListStudentsComponent },
  { path: 'student/create', component: CreateStudentComponent },
  { path: 'student/update/:id', component: UpdateStudentComponent },
  { path: 'student/delete/:id', component: DeleteStudentComponent },
  { path: 'student/get/:id', component: GetStudentComponent }
];

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuardService], children: childRoutes },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
