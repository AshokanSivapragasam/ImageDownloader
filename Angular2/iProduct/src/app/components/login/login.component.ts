import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private loginForm: FormGroup;
  constructor(private router: Router,
              private loginService: LoginService,
              private formBuilder: FormBuilder) {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
   }

  ngOnInit() {
    if (localStorage.getItem('userData')) {
      this.router.navigate(['/']);
    }
  }

  challengeIdentity() {
    const username = this.loginForm.value.username;
    const password = this.loginForm.value.password;
    const loginResponse = this.loginService.challengeIdentity(username, password);

    if (loginResponse.code === 200) {
      this.router.navigate(['/']);
      localStorage.setItem('userData', JSON.stringify(loginResponse));
    } else {
      localStorage.removeItem('userData');
    }
  }
}
