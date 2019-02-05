import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutOffersComponent } from './layout-offers.component';

describe('LayoutOffersComponent', () => {
  let component: LayoutOffersComponent;
  let fixture: ComponentFixture<LayoutOffersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutOffersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutOffersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
