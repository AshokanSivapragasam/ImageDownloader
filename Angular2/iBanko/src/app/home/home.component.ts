import { Component, OnInit, ViewChild, AfterViewInit, AfterViewChecked, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

import { ViewbalanceComponent } from '../viewbalance/viewbalance.component';
import { PayeeComponent } from '../payee/payee.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  options: FormGroup;

  constructor(private fb: FormBuilder) {
    this.options = fb.group({
      'fixed': false,
      'top': 0,
      'bottom': 0,
    });
  }

  ngOnInit() {
  }
}
