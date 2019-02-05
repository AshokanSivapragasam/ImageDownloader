import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  home: any = {
    headerLogoUri: 'http://localhost/vault/images/icons/AxisBankLogo.png',
    headerTitle: 'AXIS BANK | Net Banking',
    headerQuicklink01: 'View Demos',
    headerQuicklink02: 'Security Awareness',
    navLinks: [
      { path: 'login', label: 'LOGIN' },
      { path: 'register', label: 'REGISTER' },
    ]
  };

  loginComponent: LoginComponent;
  options: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private activatedRoute: ActivatedRoute) {
    this.options = formBuilder.group({
      'fixed': true,
      'top': 0,
      'bottom': 0,
      'opened': true
    });
  }

  ngOnInit(): void {
    this.loginComponent = new LoginComponent(this.router, this.activatedRoute);
  }

  logout() {
    this.loginComponent.logout();
  }
}
