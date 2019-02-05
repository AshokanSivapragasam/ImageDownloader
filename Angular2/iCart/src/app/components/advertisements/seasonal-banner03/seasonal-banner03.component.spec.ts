import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonalBanner03Component } from './seasonal-banner03.component';

describe('SeasonalBanner03Component', () => {
  let component: SeasonalBanner03Component;
  let fixture: ComponentFixture<SeasonalBanner03Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeasonalBanner03Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeasonalBanner03Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
