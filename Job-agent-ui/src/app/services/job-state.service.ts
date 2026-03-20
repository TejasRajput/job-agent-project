import { Injectable } from '@angular/core';
import { JobDto } from './job.services';

@Injectable({
    providedIn: 'root'
})
export class JobStateService {
    private jobs: JobDto[] = [];

    setJobs(jobs: JobDto[]) {
        this.jobs = jobs;
    }

    getJobs(): JobDto[] {
        return this.jobs;
    }
}