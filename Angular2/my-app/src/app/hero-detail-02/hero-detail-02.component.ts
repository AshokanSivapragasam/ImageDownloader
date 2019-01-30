import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-hero-detail-02',
  templateUrl: './hero-detail-02.component.html',
  styleUrls: ['./hero-detail-02.component.css']
})
export class HeroDetail02Component implements OnInit {
  name = new FormControl();

  constructor() { }

  ngOnInit() {
  }

}
