import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  emailValidator = new FormControl('', [Validators.required, Validators.email]);
  passwordHidden = true;
  returnUrl: string;
  model: any = {};
  constructor(private router: Router,
              private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.logout();
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/';
  }

  getEmailValidatorMessage() {
    return this.emailValidator.hasError('required') ? 'You must enter an email address' : (
      this.emailValidator.hasError('email') ? 'Email address is not valid' : '');
  }

  login() {
    localStorage.setItem('CurrentAxisUser', 'true');
    this.router.navigate([this.returnUrl]);
    console.log('logged in');
  }

  logout() {
    localStorage.setItem('CurrentAxisUser', 'false');
    console.log('logged ');
  }
}
