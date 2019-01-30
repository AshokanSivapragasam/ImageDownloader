import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListFramesComponent } from './list-frames.component';

describe('ListFramesComponent', () => {
  let component: ListFramesComponent;
  let fixture: ComponentFixture<ListFramesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListFramesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListFramesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
