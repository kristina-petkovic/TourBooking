import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OneTourViewComponent } from './one-tour-view.component';

describe('OneTourViewComponent', () => {
  let component: OneTourViewComponent;
  let fixture: ComponentFixture<OneTourViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OneTourViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OneTourViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
