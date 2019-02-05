import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCartItemsComponent } from './list-cart-items.component';

describe('ListCartItemsComponent', () => {
  let component: ListCartItemsComponent;
  let fixture: ComponentFixture<ListCartItemsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListCartItemsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListCartItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
