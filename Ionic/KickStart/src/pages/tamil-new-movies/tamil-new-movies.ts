import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

@Component({
    selector: 'page-tamil-new-movies',
    templateUrl: 'tamil-new-movies.html'
})

export class TamilNewMoviesPage {
    movieThumbnails: string[];
    movieTitles: string[];
    allPossibleMovieQualities: string[];
    tamilMovies: Array<{ThumbnailUrl: string, Title: string, Quality: string}>;
    
    constructor(public navCtrl: NavController, public navParams: NavParams) {
      this.movieThumbnails = ['http://localhost/vault/images/thumbnails/144-2015-228x160.jpg', 'http://localhost/vault/images/thumbnails/144-2015_228x160.jpg', 'http://localhost/vault/images/thumbnails/Baahubali-2015_228x160.jpg', 'http://localhost/vault/images/thumbnails/Bajirao-Mastani-2015-228x160.jpg', 'http://localhost/vault/images/thumbnails/Bajirao-Mastani-2015_228x160.jpg', 'http://localhost/vault/images/thumbnails/Bolt-2008_228x160.jpg', 'http://localhost/vault/images/thumbnails/Boologam-2015_228x160.jpg', 'http://localhost/vault/images/thumbnails/Eetti-2015-228x160.jpg', 'http://localhost/vault/images/thumbnails/Eetti-2016_228x160.jpg', 'http://localhost/vault/images/thumbnails/Inji-Iduppazhagi-2015-228x160.jpg', 'http://localhost/vault/images/thumbnails/Inji-Iduppazhagi-2015_228x160.jpg', 'http://localhost/vault/images/thumbnails/Inji-Murappa-2015_228x160.jpg', 'http://localhost/vault/images/thumbnails/Jackie-Chan-Adventures-Season-1-13-Episodes_228x160.jpg', 'http://localhost/vault/images/thumbnails/Kick-Ass-2-2013_228x160.jpg', 'http://localhost/vault/images/thumbnails/Naanum-Rowdydhaan-2015_228x160.jpg', 'http://localhost/vault/images/thumbnails/Oblivion-2013_228x160.jpg', 'http://localhost/vault/images/thumbnails/Ong-Bak-1-2003_228x160.jpg', 'http://localhost/vault/images/thumbnails/Ong-Bak-2-2008_228x160.jpg'];
        
      this.movieTitles = ['144-2015', '144-2016', 'Baahubali-2015', 'Bajirao-Mastani-2015', 'Bajirao-Mastani-2015', 'Bolt-2008', 'Boologam-2015', 'Eetti-2015', 'Eetti-2016', 'Inji-Iduppazhagi-2015', 'Inji-Iduppazhagi-2015', 'Inji-Murappa-2015', 'Jackie-Chan-Adventures-Season-1-13-Episodes', 'Kick-Ass-2-2013', 'Naanum-Rowdydhaan-2015', 'Oblivion-2013', 'Ong-Bak-1-2003', 'Ong-Bak-2-2008'];
        
      this.allPossibleMovieQualities = ['240p', '480p', '720p', '1080p'];
        
      this.tamilMovies = [];
      for(let idx = 1; idx < this.movieThumbnails.length; idx++) {
          this.tamilMovies.push({
            ThumbnailUrl: this.movieThumbnails[idx],
            Title: this.movieTitles[idx],
            Quality: this.allPossibleMovieQualities[Math.floor(Math.random() * this.allPossibleMovieQualities.length)]
        });
      }
    }
}