import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Url } from '../models/url.model';
import { ShortenUrlRequest } from '../models/shortenUrlRequest.model';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class ShortenedUrlService {
  private http = inject(HttpClient);
  private accountService = inject(AccountService);
  baseUrl = 'https://localhost:5001/api/ShortenUrl';

  getAllShortenedUrls(): Observable<Url[]> {
    return this.http.get<Url[]>(this.baseUrl);
  }

  getShortUrl(code: string): Observable<Url> {
    return this.http.get<Url>(`${this.baseUrl}/${code}`);
  }

  createShortUrl(request: ShortenUrlRequest): Observable<Url> {
    return this.http.post<Url>(this.baseUrl, request);
  }

  deleteShortUrl(id: string): Observable<Url> {
    return this.http.delete<Url>(
      `${this.baseUrl}/${id}`
    );
  }
}
