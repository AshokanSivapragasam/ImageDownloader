import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { of } from 'rxjs/observable/of';

import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

import { Hero } from '../heroes/hero';
import { HeroService } from '../hero.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-hero-search',
  templateUrl: './hero-search.component.html',
  styleUrls: ['./hero-search.component.css']
})
export class HeroSearchComponent implements OnInit {
  heroes$: Observable<Hero[]>;
  private searchTerms = new Subject<string>();

  constructor(private heroService: HeroService,
              private messageService: MessageService) { }

  ngOnInit() {
      this.heroes$ = this.searchTerms.pipe(
          // Wait 300ms while keystroke
          debounceTime(300),

          // Ignore if new search term remains unchanged
          distinctUntilChanged(),

          // Refresh the hero list each time when search term changes
          switchMap((searchTerm: string) => this.heroService.searchHeroes(searchTerm))
      );
  }

  searchHeroes(searchTerm: string): void {
      this.searchTerms.next(searchTerm);
    /*this.heroService.searchHeroes(searchTerm)
        .subscribe(heroes => { this.heroes$ = heroes;
                              this.messageService.add(`Receiver found #${this.heroes.length} heroes by term, ${searchTerm}`); } );*/
  }
}
