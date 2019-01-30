import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, tap, map } from 'rxjs/operators';

import { Character } from './character';
import { ConstCharacters } from './character-seed';
import { LoggerService } from './logger.service';

@Injectable()
export class CharacterService {
  apiRootUri = 'api/characters';

  constructor(private httpClient: HttpClient,
              private loggerService: LoggerService) {
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
      this.loggerService.error(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  getCharacters(): Observable<Character[]> {
    // return of(ConstCharacters);
    return this.httpClient.get<Character[]>(this.apiRootUri)
            .pipe(
              tap(characters => this.loggerService.info(`Got ${characters.length} characters from web api`)),
              catchError(this.handleError<Character[]>('getCharacters'))
            );
  }

  getCharacter(id: number): Observable<Character> {
    const characterUrl = `${this.apiRootUri}/${id}`;
    return this.httpClient.get<Character>(characterUrl)
            .pipe(
              tap(_ => this.loggerService.info(`Got a character by id, ${id}`)),
              catchError(this.handleError<Character>('getCharacter'))
            );
  }
}
