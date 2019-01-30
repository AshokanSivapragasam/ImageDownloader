import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { Media, MediaObject } from '@ionic-native/media';
import { Disc } from '../../common/disc';
import { Album } from '../../common/album';

/**
 * Generated class for the MusicZunePage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@Component({
  selector: 'music-zune-player',
  templateUrl: 'music-zune-player.html',
})
export class MusicZunePlayerPage {
  discs: Array<Disc> = [];
  selectedAlbum: Album;
  musicSystemPlayState = 'paused';
  mediaObject: MediaObject;

  constructor(public navCtrl: NavController,
              public navParams: NavParams,
              public media: Media) {
    
    this.selectedAlbum = navParams.get('selectedAlbum');
    this.discs = this.selectedAlbum.discs;
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad MusicZunePage');
  }

  playItAndPauseOtherInstances(disc: any): void {
    for(let idx = 0; idx < this.discs.length; idx += 1) {
      this.discs[idx].playState = this.discs[idx].id === disc.id ? 
                                    (this.discs[idx].playState === 'paused' || this.discs[idx].playState === 'stopped'
                                    ? 'playing': 'paused') : 'stopped';
    }

    // Init the media plugin
    this.mediaObject = this.media.create(disc.mediaUri);

    this.musicSystemPlayState = this.discs.filter((disc: Disc) => disc.playState === 'playing').length > 0 ? 'playing': 'paused';

    if(this.musicSystemPlayState === 'playing') this.mediaObject.play();
    else this.mediaObject.pause();
  }

  playCurrentMedia(): void {
    if(this.mediaObject) {
      this.mediaObject.play();

      for(let idx = 0; idx < this.discs.length; idx += 1) {
        this.discs[idx].playState = 'paused';
      }

      this.musicSystemPlayState = 'playing';
    }
  }

  playOrPauseCurrentMedia(): void {
    for(let idx = 0; idx < this.discs.length; idx += 1) {
      this.discs[idx].playState = this.discs[idx].playState === 'playing'
                                    ? 'paused': (this.discs[idx].playState === 'paused'? 'playing': 'stopped')
    }

    this.mediaObject.pause();

    this.musicSystemPlayState = this.musicSystemPlayState === 'playing'? 'paused': 'playing';
  }
}
