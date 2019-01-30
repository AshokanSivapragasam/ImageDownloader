import { Component } from '@angular/core';

import { NavController, NavParams } from 'ionic-angular';

@Component({
    selector: 'page-item-info',
    templateUrl: 'item-info.html'
})

export class ItemInfoPage {
    selectedItem: any;
    
    constructor(
        public navController: NavController,
        public navParams: NavParams){
        this.selectedItem = navParams.get('item');
    }
}