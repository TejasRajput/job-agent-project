import { Component } from '@angular/core';
import { ResumeService } from '../../services/resume.service';

@Component({
  selector: 'app-resume-upload',
  imports: [],
  templateUrl: './resume-upload.html',
  styleUrl: './resume-upload.css',
})
export class ResumeUpload {


  file!: File;

  constructor(private resumeService: ResumeService) { }

  // onFileSelected(event: any) {
  //   this.file = event.target.files[0];
  // }

  // upload() {

  //   this.resumeService.upload(this.file)
  //     .subscribe(res => {
  //       console.log(res);
  //     });
  // }

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

  // Function called when the Upload button is clicked
  onUpload(): void {
    if (!this.selectedFile) return;

    this.isUploading = true;
    this.resumeService.upload(this.selectedFile)
      .subscribe(res => {
        console.log(res);
      });

    // For now, we simulate an API call to your backend
    console.log('Sending to server...', this.selectedFile);

    setTimeout(() => {
      alert('Resume uploaded successfully!');
      this.isUploading = false;
      this.selectedFile = null;
    }, 2000);
  }
}