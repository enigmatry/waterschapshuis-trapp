import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-map-main',
  templateUrl: './map-main.component.html'
})
export class MapMainComponent {
  apiUrl = environment.apiUrl;

  constructor(private titleService: Title) { }

  setTitle(newTitle: string) {
    this.titleService.setTitle(newTitle);
  }
}
