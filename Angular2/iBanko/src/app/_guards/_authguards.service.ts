import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthguardsService implements CanActivate {

  constructor(private router: Router) { }

  canActivate (activatedRouteSnapshot: ActivatedRouteSnapshot, routerStateSnapshot: RouterStateSnapshot) {
    console.log(localStorage.getItem('CurrentAxisUser'));
    if (localStorage.getItem('CurrentAxisUser') === 'true') {
      console.log('I am in');
      return true;
    }

    this.router.navigate(['login'], { queryParams: { returnUrl: routerStateSnapshot.url } });
    return false;
  }
}
