import { Component, OnInit } from '@angular/core';

import { Hero } from '../heroes/hero';
import { HeroService } from '../hero.service';
import { MessageService } from '../message.service';
import { HeroSearchComponent } from '../hero-search/hero-search.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  heroes: Hero[] = [];
  constructor(private heroService: HeroService,
              private messageService: MessageService) { }

  ngOnInit() {
      this.getHeroes();
  }

  getHeroes(): void {
      this.heroService.getHeroes()
          .subscribe(heroes => this.heroes = heroes.slice(1, 5));
  }

  searchHeroes(searchTerm: string): void {
    this.heroService.searchHeroes(searchTerm)
        .subscribe(heroes => { this.heroes = heroes;
                              this.messageService.add(`Receiver found #${this.heroes.length} heroes by term, ${searchTerm}`); } );
  }
}
