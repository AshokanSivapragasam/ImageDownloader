import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ELEMENTS, PeriodElement } from '../../../models/element/element.models';

@Component({
  selector: 'app-delete-element',
  templateUrl: './delete-element.component.html',
  styleUrls: ['./delete-element.component.css']
})
export class DeleteElementComponent implements OnInit {
  queryParams;
  routeParams;
  deletableElementId: number;
  deletableElement: PeriodElement;

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router) {
  }

  ngOnInit() {
    this.queryParams = this.activatedRoute.snapshot.queryParams;
    this.routeParams = this.activatedRoute.snapshot.params;
    this.deletableElement = this.getElement(this.routeParams.id);
  }

  getElement(elementId: number): PeriodElement {
    // tslint:disable-next-line:triple-equals
    this.deletableElementId = ELEMENTS.findIndex(r => r.position == elementId);
    return ELEMENTS[this.deletableElementId];
  }

  deleteElement(): void {
    if (this.deletableElementId > -1) {
      ELEMENTS.splice(this.deletableElementId, 1);
    }
    this.router.navigate(['/elements']);
  }
}
