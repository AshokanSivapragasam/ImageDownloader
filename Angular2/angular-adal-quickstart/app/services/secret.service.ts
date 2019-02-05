import {Injectable} from '@angular/core';

// Application specific configuration 
@Injectable()
export class SecretService {
  public get adalConfig(): any {
    return {
      tenant: 'microsoft.onmicrosoft.com',
      clientId: '27c76cba-5610-4191-b08d-c3905aa7eeb9', 
      redirectUri: window.location.origin + '/',
      postLogoutRedirectUri: window.location.origin + '/'
    };
  }
}
