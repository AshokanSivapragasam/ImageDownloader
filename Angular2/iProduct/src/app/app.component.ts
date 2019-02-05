import { Component, OnInit } from '@angular/core';
import { RouterModule, Router, Route } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  activeRoute: string;

  constructor(private router: Router) {
    this.router.events.subscribe((changedRoute) => {
      this.routeChanged(changedRoute);
    });
  }

  ngOnInit(): void {
  }

  routeChanged(changedRoute: any): void {
    this.activeRoute = changedRoute;
  }
}
