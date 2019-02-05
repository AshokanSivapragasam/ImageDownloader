import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-add-payee',
    templateUrl: 'addpayee.component.html',
    styleUrls: ['addpayee.component.css']
})

export class AddPayeeComponent {
    // tslint:disable-next-line:no-output-on-prefix
    @Output() onAddingPayee = new EventEmitter();
    constructor (public matDialog: MatDialog) {}

    openAddPayeeDialog(): void {
        const matDialogRef = this.matDialog.open(AddPayeeDialogComponent, {
            width: '500px',
            data: {name : 'something'}
        });

        matDialogRef.afterClosed().subscribe(result => {
            this.onAddingPayee.emit(result);
            console.log(result);
        });
    }
}

@Component({
    selector: 'app-add-payee-dialog',
    templateUrl: 'addpayeedialog.component.html',
    styleUrls: ['addpayeedialog.component.css']
})
export class AddPayeeDialogComponent {
    model: any = {};
    passwordHidden = true;
    payeeBanks = [
        'Axis Bank',
        'Icici Bank',
        'Punjab National Bank',
        'Skynet'];
    payeeBankControl = new FormControl('', [Validators.required]);
    constructor(public matDialogRef: MatDialogRef<AddPayeeDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {}

    difuseDialog(): void {
        this.matDialogRef.close();
    }

    addPayee(form): void {
        console.log('Added payee' + form);
    }
}
