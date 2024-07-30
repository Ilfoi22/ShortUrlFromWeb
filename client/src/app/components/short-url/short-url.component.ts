import { Component, inject, OnInit } from '@angular/core';
import { Url } from '../../models/url.model';
import { ShortenedUrlService } from '../../services/shortened-url.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-short-url',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './short-url.component.html',
  styleUrl: './short-url.component.css',
})
export class ShortUrlComponent implements OnInit {
  private urlService = inject(ShortenedUrlService);

  accountService = inject(AccountService);
  urls: Url[] = [];

  ngOnInit(): void {
    this.loadUrls();
  }

  loadUrls() {
    this.urlService.getAllShortenedUrls().subscribe((urls) => {
      this.urls = urls;
    });
  }

  deleteUrl(id: string) {
    this.urlService.deleteShortUrl(id).subscribe(() => {
      this.loadUrls();
    });
  }
}
