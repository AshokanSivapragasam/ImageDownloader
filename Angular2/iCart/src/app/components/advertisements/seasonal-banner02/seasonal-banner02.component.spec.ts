import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeasonalBanner02Component } from './seasonal-banner02.component';

describe('SeasonalBanner02Component', () => {
  let component: SeasonalBanner02Component;
  let fixture: ComponentFixture<SeasonalBanner02Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeasonalBanner02Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeasonalBanner02Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
