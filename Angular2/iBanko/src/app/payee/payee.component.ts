import { Component, OnInit } from '@angular/core';
import { AddPayeeComponent, AddPayeeDialogComponent } from './addpayee.component';


@Component({
  selector: 'app-payee',
  templateUrl: './payee.component.html',
  styleUrls: ['./payee.component.css']
})
export class PayeeComponent implements OnInit {

  payees = [{
    name: 'User A',
    bank: 'Axis Bank',
  }, {
    name: 'User B',
    bank: 'Grahma Bank',
  }, {
    name: 'User C',
    bank: 'Punjab National Bank',
  }, {
    name: 'User D',
    bank: 'Icici Bank',
  }];
  constructor() { }

  ngOnInit() {
  }

  getPayeeInfo(payeeName: string): void {
    console.log('Info: ' + payeeName);
  }

  deletePayee(index: number): void {
    this.payees.splice(index, 1);
  }

  addPayee(payeeModel: any): void {
    const payee = {
      name: payeeModel.payeeName,
      bank: payeeModel.payeeBank
    };

    this.payees.push(payee);
  }
}
