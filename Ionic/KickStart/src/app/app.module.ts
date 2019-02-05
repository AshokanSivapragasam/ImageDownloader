import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { MyApp } from './app.component';
import { Media, MediaObject } from '@ionic-native/media';

import { HelloIonicPage } from '../pages/hello-ionic/hello-ionic';
import { ItemDetailsPage } from '../pages/item-details/item-details';
import { ItemInfoPage } from '../pages/item-info/item-info';
import { ListPage } from '../pages/list/list';
import { TamilNewMoviesPage } from '../pages/tamil-new-movies/tamil-new-movies';
import { EnglishNewMoviesPage } from '../pages/english-new-movies/english-new-movies';
import { MeetingPlannerPage } from '../pages/meeting-planner/meeting-planner';
import { MeetingDetailsPage } from '../pages/meeting-details/meeting-details';

import { MeetingSchedulePage } from '../pages/meeting-schedule/meeting-schedule';
import { MeetingProvider } from '../providers/meeting/meeting';
import { InMemoryDataService } from '../common/in-memory-data-service';
import { MusicZunePlayerPage } from '../pages/music-zune-player/music-zune-player';
import { MusicZuneHomePage } from '../pages/music-zune-home/music-zune-home';

@NgModule({
  declarations: [
    MyApp,
    HelloIonicPage,
    ItemDetailsPage,
    ItemInfoPage,
    ListPage,
    TamilNewMoviesPage,
    EnglishNewMoviesPage,
    MeetingPlannerPage,
    MeetingDetailsPage,
    MeetingSchedulePage,
    MusicZunePlayerPage,
    MusicZuneHomePage
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpClientInMemoryWebApiModule.forRoot(
      InMemoryDataService, { dataEncapsulation: false }
    ),
    IonicModule.forRoot(MyApp),
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HelloIonicPage,
    ItemDetailsPage,
    ItemInfoPage,
    ListPage,
    TamilNewMoviesPage,
    EnglishNewMoviesPage,
    MeetingPlannerPage,
    MeetingDetailsPage,
    MeetingSchedulePage,
    MusicZunePlayerPage,
    MusicZuneHomePage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    MeetingProvider,
    Media
  ]
})
export class AppModule {}
