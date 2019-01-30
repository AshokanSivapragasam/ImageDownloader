import { Component, OnInit } from '@angular/core';
import { AdvertisementBannerModel } from '../../../models/advertisements/advertisement.models';

@Component({
  selector: 'app-seasonal-banner03',
  templateUrl: './seasonal-banner03.component.html',
  styleUrls: ['./seasonal-banner03.component.css']
})
export class SeasonalBanner03Component implements OnInit {
  advertisementBanner: AdvertisementBannerModel;
  constructor() { }

  ngOnInit() {
    this.advertisementBanner = {
      imageSrc: 'https://static.lenskart.com/images/cust_mailer/26-Sep-18/premeyedesk.jpg',
      imageAlt: '18-July-18/raysun1'
    };
  }

}
