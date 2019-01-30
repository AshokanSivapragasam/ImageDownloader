import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListRecommendationsComponent } from './list-recommendations.component';

describe('ListRecommendationsComponent', () => {
  let component: ListRecommendationsComponent;
  let fixture: ComponentFixture<ListRecommendationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListRecommendationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListRecommendationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
