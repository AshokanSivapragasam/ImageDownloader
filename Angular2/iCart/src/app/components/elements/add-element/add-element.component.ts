import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ELEMENTS } from '../../../models/element/element.models';

@Component({
  selector: 'app-add-element',
  templateUrl: './add-element.component.html',
  styleUrls: ['./add-element.component.css']
})
export class AddElementComponent implements OnInit {
  addElementForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private router: Router) {
    this.addElementForm = formBuilder.group({
                            position: ['', Validators.required],
                            name: ['', Validators.required],
                            weight: ['', Validators.required],
                            symbol: ['', Validators.required]
    });
              }

  ngOnInit() {
  }

  addElement(): void {
    ELEMENTS.push({
      position: this.addElementForm.get('position').value,
      name: this.addElementForm.get('name').value,
      weight: this.addElementForm.get('weight').value,
      symbol: this.addElementForm.get('symbol').value,
      actions: null
    });

    this.router.navigate(['/elements']);
  }
}
