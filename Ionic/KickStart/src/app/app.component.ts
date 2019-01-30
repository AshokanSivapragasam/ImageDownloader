import { Component, ViewChild } from '@angular/core';

import { Platform, MenuController, Nav } from 'ionic-angular';

import { HelloIonicPage } from '../pages/hello-ionic/hello-ionic';
import { ListPage } from '../pages/list/list';
import { TamilNewMoviesPage } from '../pages/tamil-new-movies/tamil-new-movies';
import { EnglishNewMoviesPage } from '../pages/english-new-movies/english-new-movies';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { MeetingPlannerPage } from '../pages/meeting-planner/meeting-planner';
import { MusicZunePlayerPage } from '../pages/music-zune-player/music-zune-player';
import { MusicZuneHomePage } from '../pages/music-zune-home/music-zune-home';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  // make HelloIonicPage the root (or first) page
  rootPage = MeetingPlannerPage;
  pages: Array<{title: string, component: any}>;

  constructor(
    public platform: Platform,
    public menu: MenuController,
    public statusBar: StatusBar,
    public splashScreen: SplashScreen
  ) {
    this.initializeApp();

    // set our app's pages
    this.pages = [
      { title: 'Hello Ionic', component: HelloIonicPage },
      { title: 'Tamil New Movies', component: TamilNewMoviesPage },
      { title: 'English New Movies', component: EnglishNewMoviesPage },
      { title: 'My First List', component: ListPage },
      { title: 'Meeting Planner', component: MeetingPlannerPage },
      { title: 'Music Zune Player', component: MusicZuneHomePage }
    ];
  }

  initializeApp() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  openPage(page) {
    // close the menu when clicking a link from the menu
    this.menu.close();
    // navigate to the new page if it is not the current page
    this.nav.setRoot(page.component);
  }
}
