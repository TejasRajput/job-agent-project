import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class ResumeService {

    private api = "https://localhost:44317/api/resume";

    constructor(private http: HttpClient) { }

    upload(file: File) {

        const formData = new FormData();

        formData.append("file", file);

        return this.http.post(`${this.api}/upload`, formData);
    }
}