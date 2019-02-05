import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatSidenav } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout-header',
  templateUrl: './layout-header.component.html',
  styleUrls: ['./layout-header.component.css']
})
export class LayoutHeaderComponent implements OnInit {
  @Input() sideNav: MatSidenav;
  @Output() isUseridentifiedChange = new EventEmitter();
  isUseridentified = false;

  site = {
    title: 'iCart.com'
  };
  constructor(private router: Router) { }

  ngOnInit() {
    this.isUseridentified = (localStorage.getItem('user-identity') !== undefined || localStorage.getItem('user-identity').length > 0);
    this.isUseridentifiedChange.emit(this.isUseridentified);
  }

  register(): void {
    this.router.navigate(['/register']);
  }

  login(): void {
    this.router.navigate(['/login']);
  }

  logout(): void {
    localStorage.removeItem('user-identity');
    this.isUseridentified = false;
    this.isUseridentifiedChange.emit(this.isUseridentified);
    this.router.navigate(['/login']);
  }

  acquireTracker(): void {
    this.router.navigate(['/tracker']);
  }

  getWishList(): void {
    this.router.navigate(['/wishlist']);
  }

  seeWhatIsInCart(): void {
    this.router.navigate(['/cart']);
  }
}
