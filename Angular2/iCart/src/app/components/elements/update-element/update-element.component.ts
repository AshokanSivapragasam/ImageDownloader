import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PeriodElement, ELEMENTS } from '../../../models/element/element.models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-element',
  templateUrl: './update-element.component.html',
  styleUrls: ['./update-element.component.css']
})
export class UpdateElementComponent implements OnInit {
  queryParams;
  routeParams;
  elementToBeUpdated: PeriodElement;
  updatableElementId: number;
  updateElementForm: FormGroup;


  constructor(private activatedRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private router: Router) {
    console.log('Route Id: ' + this.activatedRoute.snapshot.params['id']);
    this.updateElementForm = formBuilder.group({
                              position: ['', Validators.required],
                              name: ['', Validators.required],
                              weight: ['', Validators.required],
                              symbol: ['', Validators.required]
                });
              }

  ngOnInit() {
    this.queryParams = this.activatedRoute.snapshot.queryParams;
    this.routeParams = this.activatedRoute.snapshot.params;

    this.elementToBeUpdated = this.getElement(this.routeParams.id);
    this.updateElementForm.setValue({
      position: this.elementToBeUpdated.position,
      name: this.elementToBeUpdated.name,
      weight: this.elementToBeUpdated.weight,
      symbol: this.elementToBeUpdated.symbol
    });
  }

  getElement(elementId: number): PeriodElement {
    // tslint:disable-next-line:triple-equals
    this.updatableElementId = ELEMENTS.findIndex(r => r.position == elementId);
    return ELEMENTS[this.updatableElementId];
  }

  updateElement(): void {
    ELEMENTS[this.updatableElementId] = {
      position: this.updateElementForm.get('position').value,
      name: this.updateElementForm.get('name').value,
      weight: this.updateElementForm.get('weight').value,
      symbol: this.updateElementForm.get('symbol').value,
      actions: null
    };
    this.router.navigate(['/elements']);
  }
}
