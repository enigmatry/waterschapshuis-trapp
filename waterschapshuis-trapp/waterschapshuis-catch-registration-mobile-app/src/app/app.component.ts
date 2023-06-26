import { Component } from '@angular/core';
import { CacheService } from './cache/cache.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent {
  constructor(cache: CacheService) {
    cache.setDefaultTTL(60 * 60); // set default cache TTL for 1 hour
  }
}
