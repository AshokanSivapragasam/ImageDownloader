import { Component, OnInit, Input } from '@angular/core';
import { Character } from '../character';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { CharacterService } from '../character.service';


@Component({
  selector: 'app-character-editor',
  templateUrl: './character-editor.component.html',
  styleUrls: ['./character-editor.component.css']
})
export class CharacterEditorComponent implements OnInit {
  model: Character;
  allpowers: String[];
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;

  // Enter, comma
  separatorKeysCodes = [ENTER, COMMA];

  constructor(private activatedRoute: ActivatedRoute,
              private characterService: CharacterService,
              private location: Location) { }

  ngOnInit() {
    this.getCharacter();
  }

  getCharacter(): void {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.characterService.getCharacter(id)
    .subscribe(character => {
                  this.model = character;
                  this.allpowers = character.powers;
                });
  }

  addSidekick(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our fruit
    if ((value || '').trim()) {
      this.model.sidekicks.push(value.trim());
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  removeSidekick(sidekick: any): void {
    const index = this.model.sidekicks.indexOf(sidekick);

    if (index >= 0) {
      this.model.sidekicks.splice(index, 1);
    }
  }

  addVillian(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our fruit
    if ((value || '').trim()) {
      this.model.villians.push(value.trim());
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  removeVillian(villian: any): void {
    const index = this.model.villians.indexOf(villian);

    if (index >= 0) {
      this.model.villians.splice(index, 1);
    }
  }

  goBack(): void {
    this.location.back();
  }

  getdiagnostic() { return JSON.stringify(this.model); }
}
