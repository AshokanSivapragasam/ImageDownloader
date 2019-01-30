import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, tap, map } from 'rxjs/operators';

import { Meeting } from '../../common/meeting';

/*
  Generated class for the MeetingPlannerProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class MeetingProvider {
  apiRootUri = 'api/meetings';

  constructor(public httpClient: HttpClient) {
    console.log('Hello MeetingPlannerProvider Provider');    
  }

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

      // TODO: better job of transforming error for user consumption
      console.error(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  getMeetings(): Observable<Meeting[]> {
    return this.httpClient.get<Meeting[]>(this.apiRootUri)
            .pipe(
              tap(meetings => console.info(`Got ${meetings.length} meetings from web api`)),
              catchError(this.handleError<Meeting[]>('getMeetings'))
            );
  }

  getMeeting(id: number): Observable<Meeting> {
    const meetingUrl = `${this.apiRootUri}/${id}`;
    return this.httpClient.get<Meeting>(meetingUrl)
            .pipe(
              tap(_ => console.info(`Got a meeting by id ${id} from web api`)),
              catchError(this.handleError<Meeting>('getMeeting'))
            );
  }

  addMeeting(meeting: Meeting): Observable<Meeting> {
    console.log('Adding a schedule, ' + meeting.title + '');
      const httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };
      return this.httpClient.post<Meeting>(this.apiRootUri, meeting, httpOptions)
      .pipe(
          tap(_ => console.log(`Added the schedule, ${meeting.title}`)),
          catchError(this.handleError<any>('addMeeting'))
      );
  }
}
