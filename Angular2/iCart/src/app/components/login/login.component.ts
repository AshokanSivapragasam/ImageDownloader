import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  hide = false;
  loginForm: FormGroup;

  constructor(private router: Router,
              private formBuilder: FormBuilder,
              private loginService: LoginService) {
    this.loginForm = formBuilder.group({
                      email: ['abc@gmail.com', Validators.required],
                      password: ['', Validators.required]
                    });
  }

  ngOnInit() {
    if (localStorage.getItem('user-identity')) {
      this.router.navigate(['/elements']);
    }
  }

  challengeIdentity(): void {
    const _email = this.loginForm.value.email;
    const _password = this.loginForm.value.password;
    const isValidUser = this.loginService.challengeIdentity(_email, _password);
    if (isValidUser) {
      localStorage.setItem('user-identity', JSON.stringify({ email: _email, password: _password, isIdentityProved: isValidUser }));
      this.router.navigate(['/elements']);
    } else {
      localStorage.removeItem('user-identity');
      this.router.navigate(['/login']);
    }
  }

  register(): void {
    console.log('clicked');
    event.preventDefault();
  }
}
