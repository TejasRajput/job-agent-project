import { Component } from '@angular/core';
import { ResumeService } from '../../services/resume.service';
import { FormsModule } from '@angular/forms';
import { JobService } from '../../services/job.services';
import { Router } from '@angular/router';
import { JobStateService } from '../../services/job-state.service';

@Component({
  selector: 'app-resume-upload',
  imports: [FormsModule],
  templateUrl: './resume-upload.html',
  styleUrl: './resume-upload.css',
})
export class ResumeUpload {


  file!: File;

  constructor(
    private resumeService: ResumeService,
    private jobService: JobService,
    private router: Router,
    private jobState: JobStateService,
  ) { }


  selectedFile: File | null = null;
  errorMessage: string | null = null;
  isUploading: boolean = false;

  // Function called when the user selects a file
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      const file = input.files[0];

      // Validation: Ensure it is a PDF
      if (file.type !== 'application/pdf') {
        this.errorMessage = 'Only PDF files are allowed.';
        this.selectedFile = null;
        return;
      }

      this.errorMessage = null;
      this.selectedFile = file;
      console.log('File selected:', file.name);
    }
  }

  // resume-upload.component.ts
  onUpload(): void {
    if (!this.selectedFile) return;

    this.isUploading = true;
    this.errorMessage = null;

    this.resumeService.upload(this.selectedFile).subscribe({
      next: (res: any) => {
        console.log('Uploaded:', res);

        // After upload, automatically fetch jobs
        const filePath = res.filePath; // return from API
        this.jobService.recommendJobs(filePath).subscribe({
          next: (jobs) => {
            this.jobState.setJobs(jobs);
            console.log('Jobs:', jobs);
            this.router.navigate(['/job-recommendations'], { state: { jobs } });
          },
          error: (err) => {
            console.error('Job fetch error:', err);
          }
        });

        this.isUploading = false;
        this.selectedFile = null;
      },
      error: (err) => {
        console.error(err);
        this.errorMessage = 'Upload failed';
        this.isUploading = false;
      }
    });
  }
}