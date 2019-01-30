import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { PeriodElement, ELEMENTS } from '../../../models/element/element.models';

@Component({
  selector: 'app-list-elements',
  templateUrl: './list-elements.component.html',
  styleUrls: ['./list-elements.component.css']
})
export class ListElementsComponent implements OnInit {
  displayedColumns: string[] = ['select', 'position', 'name', 'weight', 'symbol', 'actions'];
  shouldMultiSelectEnabled: boolean;
  dataSource = new MatTableDataSource<PeriodElement>(ELEMENTS);
  selection = new SelectionModel<PeriodElement>(true, []);
  constructor(private router: Router) { }

  ngOnInit() {
    this.shouldMultiSelectEnabled = this.displayedColumns[0].toString() === 'select';
  }

  isAllSelected(): boolean {
    const numberOfRowsSelected = this.selection.selected.length;
    const totalNumberOfRows = this.dataSource.data.length;
    return numberOfRowsSelected === totalNumberOfRows;
  }

  masterToggle(): void {
    this.isAllSelected() ? this.selection.clear() : this.dataSource.data.forEach(row => this.selection.select(row));
  }

  applyFilter(filterBy: string) {
    this.dataSource.filter = filterBy.trim().toLowerCase();
  }

  addElement(): void {
    this.router.navigate(['/elements/add']);
  }

  updateElement(periodElementId: number): void {
    this.router.navigate(['/elements/' + periodElementId + '/update']);
  }

  deleteElement(periodElementsArrayIndex: number, periodElementId: number): void {
    if (confirm('Are you sure that you want to delete this element from periodic table?')) {
      ELEMENTS.splice(periodElementsArrayIndex, 1);
      this.dataSource._updateChangeSubscription();
    } else {
      this.router.navigate(['/elements/' + periodElementId + '/delete']);
    }
  }
}
