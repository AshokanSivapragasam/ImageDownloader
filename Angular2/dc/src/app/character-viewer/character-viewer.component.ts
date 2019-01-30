import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Character } from '../character';
import { CharacterService } from '../character.service';

@Component({
  selector: 'app-character-viewer',
  templateUrl: './character-viewer.component.html',
  styleUrls: ['./character-viewer.component.css']
})
export class CharacterViewerComponent implements OnInit {
  character: Character;

  constructor(private activatedRoute: ActivatedRoute,
              private characterService: CharacterService,
              private location: Location) { }

  ngOnInit() {
    this.getCharacter();
  }

  getCharacter(): void {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.characterService.getCharacter(id)
    .subscribe(character => this.character = character);
  }
}
