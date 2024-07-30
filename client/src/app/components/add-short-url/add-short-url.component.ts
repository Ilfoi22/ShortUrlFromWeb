import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ShortenedUrlService } from '../../services/shortened-url.service';
import { ShortenUrlRequest } from '../../models/shortenUrlRequest.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-short-url',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-short-url.component.html',
  styleUrl: './add-short-url.component.css',
})
export class AddShortUrlComponent {
  private shortenedUrlService = inject(ShortenedUrlService);
  private toastr = inject(ToastrService);

  url: string = '';

  addUrl() {
    const request: ShortenUrlRequest = { url: this.url };
    this.shortenedUrlService.createShortUrl(request).subscribe({
      next: () => {
        this.toastr.success('URL shortened successfully!');
      },
      error: (error) => {
        console.error(error);
        this.toastr.error('Failed to shorten URL.');
      },
    });
  }
}
