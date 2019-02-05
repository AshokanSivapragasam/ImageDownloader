import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Hero } from '../heroes/hero';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.css']
})
export class HeroDetailComponent implements OnInit {
  hero: Hero;
  constructor(private activatedRoute: ActivatedRoute,
              private heroService: HeroService,
              private location: Location) { }

  ngOnInit() {
      this.getHero();
  }

  getHero(): void {
      const id = +this.activatedRoute.snapshot.paramMap.get('id');
      this.heroService.getHero(id).subscribe(hero => this.hero = hero);
  }

  save(hero: Hero): void {
      this.heroService.updateHero(hero).subscribe(() => this.goBack());
  }

  goBack(): void {
      this.location.back();
  }
}
