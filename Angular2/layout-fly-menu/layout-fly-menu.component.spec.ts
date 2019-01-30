import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutFlyMenuComponent } from './layout-fly-menu.component';

describe('LayoutFlyMenuComponent', () => {
  let component: LayoutFlyMenuComponent;
  let fixture: ComponentFixture<LayoutFlyMenuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LayoutFlyMenuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LayoutFlyMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
