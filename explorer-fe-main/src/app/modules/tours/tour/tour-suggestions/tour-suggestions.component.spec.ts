import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TourSuggestionsComponent } from './tour-suggestions.component';

describe('TourSuggestionsComponent', () => {
  let component: TourSuggestionsComponent;
  let fixture: ComponentFixture<TourSuggestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TourSuggestionsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TourSuggestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
