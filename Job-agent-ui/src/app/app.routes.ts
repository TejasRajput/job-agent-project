import { Routes } from '@angular/router';
import { JobMatches } from './pages/job-matches/job-matches';
import { Dashboard } from './pages/dashboard/dashboard';
import { ResumeUpload } from './pages/resume-upload/resume-upload';
import { JobRecommendations } from './pages/job-recommendations/job-recommendations';

export const routes: Routes = [
    {
        path: 'upload',          // The URL will be localhost:4200/upload
        component: ResumeUpload
    },
    {
        path: 'job-matches',
        component: JobMatches
    },
    {
        path: 'dashboard',
        component: Dashboard
    },
    {
        path: 'job-recommendations', component: JobRecommendations
    },
    {
        path: '**',
        redirectTo: 'upload'
    },
];
