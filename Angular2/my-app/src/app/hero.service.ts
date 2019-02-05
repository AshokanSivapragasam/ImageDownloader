import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Hero } from './heroes/hero';
import { HEROES } from './heroes/mock-heroes';
import { MessageService } from './message.service';

@Injectable()
export class HeroService {
  private heroesUrl = 'api/heroes';
  
  constructor(private httpClient: HttpClient,
              private messageService: MessageService) { }

  getHeroes(): Observable<Hero[]> {
      this.messageService.add('Hero Service is exporting heroes');
      //return of(HEROES);
      return this.httpClient.get<Hero[]>(this.heroesUrl)
          .pipe(
          tap(heroes => this.messageService.add(`fetched heroes`)),
          catchError(this.handleError('getHeroes', [])));
  }

  getHero(id: number): Observable<Hero> {
      this.messageService.add('Finding the hero by id#' + id + ' ');
      const heroUrl = `${this.heroesUrl}/${id}`;
      //return of(HEROES.find(hero => hero.id === id));
      return this.httpClient.get<Hero>(heroUrl)
      .pipe(
          tap(_ => this.messageService.add(`fetched hero by ${id}`)),
          catchError(this.handleError<Hero>('getHero'))
      );
  }

  addHero(hero: Hero): Observable<Hero> {
      this.messageService.add('Adding the hero ' + hero.name + '');
      const httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };
      return this.httpClient.post<Hero>(this.heroesUrl, hero, httpOptions)
      .pipe(
          tap(_ => this.messageService.add(`Added hero, ${hero.name}`)),
          catchError(this.handleError<any>('addHero'))
      );
  }

  deleteHero(hero: Hero | number): Observable<Hero> {
      const heroId = typeof(hero) === 'number' ? hero : hero.id;
      this.messageService.add('Deleting the hero by id ' + heroId + '');
      const heroUrl = `${this.heroesUrl}/${heroId}`;
      const httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };
      return this.httpClient.delete<Hero>(heroUrl, httpOptions)
      .pipe(
          tap(_ => this.messageService.add(`Deleted hero, ${heroId}`)),
          catchError(this.handleError<any>('deleteHero'))
      );
  }

  updateHero(hero: Hero): Observable<any> {
      this.messageService.add('Updating the hero ' + hero.name + '');
      const httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };
      return this.httpClient.put<Hero>(this.heroesUrl, hero, httpOptions)
      .pipe(
          tap(_ => this.messageService.add(`Updated hero, ${hero.name}`)),
          catchError(this.handleError<any>('updateHero'))
      );
  }

  searchHeroes(searchTerm: string): Observable<Hero[]> {
      this.messageService.add('Searching the heroes by term, ' + searchTerm + '');
      const httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };
      const filteredHeroesUrl = `${this.heroesUrl}/?name=${searchTerm}`;
      return this.httpClient.get<Hero[]>(filteredHeroesUrl, httpOptions)
      .pipe(
          tap(_ => this.messageService.add(`Searched heroes, ${searchTerm}`)),
          catchError(this.handleError<any>('searchHeroes'))
      );
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
        this.messageService.add(`${operation} failed: ${error.message}`);

        // Let the app keep running by returning an empty result.
        return of(result as T);
      };
    }
}
