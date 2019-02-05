import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeroDetail02Component } from './hero-detail-02.component';

describe('HeroDetail02Component', () => {
  let component: HeroDetail02Component;
  let fixture: ComponentFixture<HeroDetail02Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeroDetail02Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeroDetail02Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
