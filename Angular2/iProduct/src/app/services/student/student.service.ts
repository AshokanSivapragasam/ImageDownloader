import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { Student } from '../../models/student';

@Injectable()
export class StudentService {
  private appRootUri = 'api/students';

  constructor(private httpClient: HttpClient) { }

  getAll(): Observable<Student[]> {
    return this.httpClient.get<Student[]>(this.appRootUri)
      .pipe(tap(data => console.log(`Got all ${data.length} students`)));
  }

  get(id: number): Observable<Student> {
    const studentUrl = `${this.appRootUri}/${id}`;
    return this.httpClient.get<Student>(studentUrl)
      .pipe(tap(_ => console.log(`Got student by id, ${id}`)));
  }

  delete(id: number): void {
    const studentUrl = `${this.appRootUri}/${id}`;
    this.httpClient.delete(studentUrl)
      .pipe(tap(_ => console.log(`Deleted student by id, ${id}`)));
  }
}
