import { NgModule } from '@angular/core';
import { RouterModule, Routes, Route } from '@angular/router';

import { CharactersComponent } from './characters/characters.component';
import { CharacterEditorComponent } from './character-editor/character-editor.component';
import { CharacterViewerComponent } from './character-viewer/character-viewer.component';

const routes: Routes = [
  { path: '', redirectTo: '/characters', pathMatch: 'full' },
  { path: 'characters', component: CharactersComponent },
  { path: 'character-editor/:id', component: CharacterEditorComponent },
  { path: 'character-viewer/:id', component: CharacterViewerComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
