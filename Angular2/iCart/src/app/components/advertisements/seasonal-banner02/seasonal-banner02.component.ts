import { Component, OnInit } from '@angular/core';
import { AdvertisementBannerModel } from '../../../models/advertisements/advertisement.models';

@Component({
  selector: 'app-seasonal-banner02',
  templateUrl: './seasonal-banner02.component.html',
  styleUrls: ['./seasonal-banner02.component.css']
})
export class SeasonalBanner02Component implements OnInit {
  advertisementBanners: AdvertisementBannerModel[];
  constructor() { }

  ngOnInit() {
    this.advertisementBanners = [{
      imageSrc: 'https://static.lenskart.com/images/cust_mailer/7-Dec-17/try3dtop.jpg',
      imageAlt: 'try3dtop'
    }, {
      imageSrc: 'https://static5.lenskart.com/images/cust_mailer/19-Dec-18/cldesk-small.jpg',
      imageAlt: 'cldesk-small'
    }, {
      imageSrc: 'https://static.lenskart.com/images/cust_mailer/15-June-17/top_03.jpg',
      imageAlt: 'top_03'
    }];
  }

}
