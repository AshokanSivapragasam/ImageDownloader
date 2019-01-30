import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayFrameComponent } from './display-frame.component';

describe('DisplayFrameComponent', () => {
  let component: DisplayFrameComponent;
  let fixture: ComponentFixture<DisplayFrameComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DisplayFrameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayFrameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
