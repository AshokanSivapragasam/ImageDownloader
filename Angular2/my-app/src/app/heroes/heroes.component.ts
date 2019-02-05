import { Component, OnInit } from '@angular/core';
import { Hero } from './hero';
import { HeroService } from '../hero.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  heroes: Hero[];

  constructor(private heroService: HeroService,
              public messageService: MessageService) { }

  ngOnInit() {
    this.getHeroes();
  }

  getHeroes(): void {
    this.heroService.getHeroes()
        .subscribe(heroes => { this.heroes = heroes; this.messageService.add('Receiver received all #' + heroes.length + ' heroes'); } );
  }

  addHero(heroName: string): void {
    this.heroService.addHero({name: heroName} as Hero)
        .subscribe(hero => { this.heroes.push(hero); this.messageService.add('Receiver added ' + heroName + ''); } );
  }

  deleteHero(hero: Hero): void {
    this.heroes = this.heroes.filter(h => h !== hero);
    this.heroService.deleteHero(hero)
        .subscribe(() => { this.messageService.add('Receiver deleted'); } );
  }


}
