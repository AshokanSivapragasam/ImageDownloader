import { BrowserModule, DomSanitizer } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Ng2CarouselamosModule } from 'ng2-carouselamos';
import {
  MatAutocompleteModule,
  MatBadgeModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule,
  MatIconRegistry
 } from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { LoginService } from './services/login/login.service';
import { HomeComponent } from './components/home/home.component';
import { LayoutFooterComponent } from './components/layout-footer/layout-footer.component';
import { LayoutHeaderComponent } from './components/layout-header/layout-header.component';
import { ListElementsComponent } from './components/elements/list-elements/list-elements.component';
import { RegisterComponent } from './components/register/register.component';
import { TrackerComponent } from './components/tracker/tracker.component';
import { WishlistComponent } from './components/wishlist/wishlist.component';
import { ListCartItemsComponent } from './components/cart/list-cart-items/list-cart-items.component';
import { AddElementComponent } from './components/elements/add-element/add-element.component';
import { UpdateElementComponent } from './components/elements/update-element/update-element.component';
import { DeleteElementComponent } from './components/elements/delete-element/delete-element.component';
import { LayoutOffersComponent } from './components/layout-offers/layout-offers.component';
import { LayoutFlyMenuComponent } from './components/layout-fly-menu/layout-fly-menu.component';
import { LayoutTwolayerFlyMenuComponent } from './components/layout-twolayer-fly-menu/layout-twolayer-fly-menu.component';
import { HttpClientModule } from '@angular/common/http';
import { ListFramesComponent } from './components/frames/list-frames/list-frames.component';
import { ThirukkuralComponent } from './components/thirukkural/thirukkural.component';
import { ThirukkuralService } from './services/thirukkural/thiikkural.service';
import { ListRecommendationsComponent } from './components/recommendations/list-recommendations/list-recommendations.component';
import { SeasonalBanner01Component } from './components/advertisements/seasonal-banner01/seasonal-banner01.component';
import { SeasonalBanner02Component } from './components/advertisements/seasonal-banner02/seasonal-banner02.component';
import { SeasonalBanner03Component } from './components/advertisements/seasonal-banner03/seasonal-banner03.component';
import { DisplayFrameComponent } from './components/frames/display-frame/display-frame.component';
import { InMemoryDataService } from './services/in-memory-data-frames.service';

@NgModule({
  exports: [
    MatAutocompleteModule,
    MatBadgeModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule
  ]
})
export class CustomMaterialModule { }


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    LayoutHeaderComponent,
    LayoutFooterComponent,
    ListElementsComponent,
    AddElementComponent,
    UpdateElementComponent,
    DeleteElementComponent,
    RegisterComponent,
    TrackerComponent,
    WishlistComponent,
    ListCartItemsComponent,
    LayoutOffersComponent,
    LayoutFlyMenuComponent,
    LayoutTwolayerFlyMenuComponent,
    ListFramesComponent,
    DisplayFrameComponent,
    ThirukkuralComponent,
    ListRecommendationsComponent,
    SeasonalBanner01Component,
    SeasonalBanner02Component,
    SeasonalBanner03Component
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientInMemoryWebApiModule.forRoot(
      InMemoryDataService, {dataEncapsulation: false, passThruUnknownUrl: true}),
    NoopAnimationsModule,
    AppRoutingModule,
    CustomMaterialModule,
    Ng2CarouselamosModule
  ],
  providers: [LoginService, ThirukkuralService],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private matIconRegistry: MatIconRegistry, domSanitizer: DomSanitizer) {
    matIconRegistry.addSvgIconSet(domSanitizer.bypassSecurityTrustResourceUrl('~/../assets/mdi.svg'));
  }
}
