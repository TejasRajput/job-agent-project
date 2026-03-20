import { Component, signal } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  // FIX: Change 'templateUrl' to 'template'
  template: `
    <h1>AI Job Agent</h1>
    <nav>
      <a routerLink="/dashboard">Dashboard</a> |
      <a routerLink="/upload">Upload Resume</a> |
      <a routerLink="/job-matches">Job Matches</a> |
      <a routerLink="/job-recommendations">Job Recommendations</a>
    </nav>
    <hr>
    <router-outlet></router-outlet>
  `,
  styleUrls: ['./app.css'],
})
export class App { }