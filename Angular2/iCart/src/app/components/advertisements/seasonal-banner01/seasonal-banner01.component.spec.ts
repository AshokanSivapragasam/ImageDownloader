import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonalBanner01Component } from './seasonal-banner01.component';

describe('SeasonalBanner01Component', () => {
  let component: SeasonalBanner01Component;
  let fixture: ComponentFixture<SeasonalBanner01Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeasonalBanner01Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeasonalBanner01Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
