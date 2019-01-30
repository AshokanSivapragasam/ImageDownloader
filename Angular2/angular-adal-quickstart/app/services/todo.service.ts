import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, tap, map } from 'rxjs/operators';

@Injectable()
export class TodoService {
    apiRootUri = 'https://ei.microsoft.com/ToDoServiceWithDaemon/api/TodoList';

    constructor(public httpClient: HttpClient) { }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

    getTodoList(accessToken: string): Observable<any[]> {
        const httpOptions = {
            headers: new HttpHeaders({ 'Authorization' : 'Bearer ' + accessToken + '' })
          };

        return this.httpClient.get<any[]>(this.apiRootUri, httpOptions)
        .pipe(
            tap(todoList => console.log(`Got ${ todoList.length } items`)),
            catchError(this.handleError<any[]>('getTodoList'))
        );
    }

    addTodoList(title: string, accessToken: string): Observable<any> {
        const httpOptions = {
            headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization' : 'Bearer ' + accessToken + '' })
          };

        let _body_: any = {Title : title};

        return this.httpClient.post(this.apiRootUri, _body_, httpOptions)
        .pipe(
            tap(_ => console.log(`Must have been added`)),
            catchError(this.handleError<any[]>('addTodoList'))
        );
    }
}