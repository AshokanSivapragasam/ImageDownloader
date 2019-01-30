import { Injectable } from '@angular/core';

@Injectable()
export class LoginService {

  constructor() { }

  challengeIdentity(username: string, password: string): any {
    if (username === 'ashok@gmail.com' && password === '1time@p') {
      return {
        code: 200,
        message: 'Login successful',
        username: username
      };
    } else {
      return {
        code: 400,
        message: 'Login attempt failed',
        username: username
      };
    }
  }
}
