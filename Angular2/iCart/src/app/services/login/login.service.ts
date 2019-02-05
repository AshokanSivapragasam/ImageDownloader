import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor() { }

  challengeIdentity(email: string, password: string): boolean {
    console.log(`${email} - ${password}`);
    return email === 'abc@gmail.com' && password === '1time@p';
  }
}
