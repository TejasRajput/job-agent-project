import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface JobDto {
    title: string;
    company: string;
    location: string;
    requiredSkills: string[];
}

@Injectable({
    providedIn: 'root'
})
export class JobService {
    private apiUrl = 'https://localhost:44317/api/job/recommend';

    constructor(private http: HttpClient) { }

    recommendJobs(filePath: string): Observable<JobDto[]> {
        return this.http.post<JobDto[]>(this.apiUrl, { filePath });
    }
}