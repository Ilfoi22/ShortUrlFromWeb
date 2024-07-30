import { Routes } from '@angular/router';
import { ShortUrlComponent } from './components/short-url/short-url.component';
import { AddShortUrlComponent } from './components/add-short-url/add-short-url.component';
import { authGuard } from './guard/auth.guard';
import { AboutComponent } from './components/about/about.component';

export const routes: Routes = [
  { path: '', component: ShortUrlComponent },
  {
    path: 'add-short-url',
    component: AddShortUrlComponent,
    canActivate: [authGuard],
  },
  { path: 'about', component: AboutComponent },
  { path: '**', component: ShortUrlComponent },
];
