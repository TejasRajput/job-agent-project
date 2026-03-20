import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobRecommendations } from './job-recommendations';

describe('JobRecommendations', () => {
  let component: JobRecommendations;
  let fixture: ComponentFixture<JobRecommendations>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [JobRecommendations],
    }).compileComponents();

    fixture = TestBed.createComponent(JobRecommendations);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
