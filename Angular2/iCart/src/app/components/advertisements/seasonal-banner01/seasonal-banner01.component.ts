import { Component, OnInit } from '@angular/core';
import { AdvertisementBannerModel } from '../../../models/advertisements/advertisement.models';

@Component({
  selector: 'app-seasonal-banner01',
  templateUrl: './seasonal-banner01.component.html',
  styleUrls: ['./seasonal-banner01.component.css']
})
export class SeasonalBanner01Component implements OnInit {
  advertisementBanners: AdvertisementBannerModel[];

  constructor() { }

  ngOnInit() {
    this.advertisementBanners = [{
      imageSrc: '~/../assets/chrishome_01.jpg',
      imageAlt: 'chrishome_01'
    }, {
      imageSrc: '~/../assets/chrishome_02.jpg',
      imageAlt: 'chrishome_02'
    }, {
      imageSrc: '~/../assets/chrishome_03.jpg',
      imageAlt: 'chrishome_03'
    }, {
      imageSrc: '~/../assets/chrishome_04.jpg',
      imageAlt: 'chrishome_04'
    }, {
      imageSrc: '~/../assets/katrina_optimize.gif',
      imageAlt: 'chrishome_04'
    }];
  }
}
