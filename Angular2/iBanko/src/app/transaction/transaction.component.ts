import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {
  model: any = {};
  selectedPayeeControl = new FormControl('', [Validators.required]);
  constructor() { }

  ngOnInit() {
    this.model.payerAccount = 'Ashok 987987XXXXXX';
  }

  transferMoney(): void {
    console.log('Money transferred');
  }
}
