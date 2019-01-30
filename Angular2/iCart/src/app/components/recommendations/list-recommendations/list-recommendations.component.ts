import { Component, OnInit } from '@angular/core';
import { RecommendationModel } from '../../../models/recommendation/recommendation.models';

@Component({
  selector: 'app-list-recommendations',
  templateUrl: './list-recommendations.component.html',
  styleUrls: ['./list-recommendations.component.css']
})
export class ListRecommendationsComponent implements OnInit {
  recommendations: RecommendationModel[];

  constructor() { }

  ngOnInit() {
    this.recommendations = [{
      imageAlt: 'Photo of First Frame Free',
      imageSrc: '~/../assets/img_first_frame_free.jpg',
      title: 'First Frame Free',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Colored Contacts',
      imageSrc: '~/../assets/img_colored_contacts.jpg',
      title: 'Colored Contacts',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of BLU Reading Glasses',
      imageSrc: '~/../assets/img_BLU_reading_glasses.jpg',
      title: 'BLU Reading Glasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Power Sunglasses',
      imageSrc: '~/../assets/img_power_sunglasses.jpg',
      title: 'Power Sunglasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Aviator Sunglasses',
      imageSrc: '~/../assets/img_aviator_sunglasses.jpg',
      title: 'Aviator Sunglasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Round Eyeglasses',
      imageSrc: '~/../assets/img_round_eyeglasses.jpg',
      title: 'Round Eyeglasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Computer Glasses',
      imageSrc: '~/../assets/img_computer_glasses.jpg',
      title: 'Computer Glasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Rayban Sunglasses',
      imageSrc: '~/../assets/img_rayban_sunglasses.jpg',
      title: 'Rayban Sunglasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Rayban Eyeglasses',
      imageSrc: '~/../assets/img_rayban_eyeglasses.jpg',
      title: 'Rayban Eyeglasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Lens Solution',
      imageSrc: '~/../assets/img_lens_solution.jpg',
      title: 'Lens Solution',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Polarized Sunglasses',
      imageSrc: '~/../assets/img_polarized_sunglasses.jpg',
      title: 'Polarized Sunglasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Premium Eyeglasses',
      imageSrc: '~/../assets/img_premium_eyeglasses.jpg',
      title: 'Premium Eyeglasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Premium Sunglasses',
      imageSrc: '~/../assets/img_premium_sunglasses.jpg',
      title: 'Premium Sunglasses',
      subtitle: 'from Rs.499'
    }, {
      imageAlt: 'Photo of Air Flex Collection',
      imageSrc: '~/../assets/img_air_flex_collection.jpg',
      title: 'Air Flex Collection',
      subtitle: 'from Rs.499'
    }];
  }

}
