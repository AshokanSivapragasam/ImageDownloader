import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-viewbalance',
  templateUrl: './viewbalance.component.html',
  styleUrls: ['./viewbalance.component.css']
})
export class ViewbalanceComponent implements OnInit {
  balance: number;
  debt: number;
  creditPoints: number;
  constructor() { }

  ngOnInit() {
    this.balance = 1023478;
    this.debt = 0;
    this.creditPoints = 276;
  }
}
