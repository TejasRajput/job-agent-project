import { Component } from '@angular/core';
import { JobDto } from '../../services/job.services';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from 'express';
import { JobStateService } from '../../services/job-state.service';

@Component({
  selector: 'app-job-recommendations',
  imports: [FormsModule, CommonModule],
  templateUrl: './job-recommendations.html',
  styleUrl: './job-recommendations.css',
})
export class JobRecommendations {
  jobs: JobDto[] = [];
  loading: boolean = false;
  error: string = '';

  constructor(private jobState: JobStateService) {
    this.jobs = this.jobState.getJobs();
  }
}
