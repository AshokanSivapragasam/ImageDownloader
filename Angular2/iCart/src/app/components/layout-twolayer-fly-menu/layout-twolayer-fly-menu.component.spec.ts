import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutTwolayerFlyMenuComponent } from './layout-twolayer-fly-menu.component';

describe('LayoutTwolayerFlyMenuComponent', () => {
  let component: LayoutTwolayerFlyMenuComponent;
  let fixture: ComponentFixture<LayoutTwolayerFlyMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutTwolayerFlyMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutTwolayerFlyMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
