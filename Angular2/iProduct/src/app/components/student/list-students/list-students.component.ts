import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';

import { StudentService } from '../../../services/student/student.service';
import { Student } from '../../../models/student';


@Component({
  selector: 'app-list-students',
  templateUrl: './list-students.component.html',
  styleUrls: ['./list-students.component.css']
})
export class ListStudentsComponent implements OnInit {
  students: Student[];
  displayedColumns: string[] = ['select', 'id', 'name', 'age', 'course'];
  dataSource: any;
  selection = new SelectionModel<Student>(true, []);

  /**
     * Pre-defined columns list for user table
     */
    columnNames = [{
      id: 'id',
      value: 'No.'

    }, {
      id: 'name',
      value: 'Name'
    },
    {
      id: 'age',
      value: 'Age'
    },
    {
      id: 'course',
      value: 'Course'
    }];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private studentService: StudentService) {
  }

  ngOnInit() {
    this.getAllStudents();
  }

  getAllStudents(): void {
    this.studentService.getAll().subscribe(
      students => {
        this.students = students;
        console.log(`${this.students.length}`);
        this.dataSource = new MatTableDataSource<Student>(this.students);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }
    );
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }
}
