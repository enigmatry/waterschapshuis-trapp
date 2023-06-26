import { Component, OnInit, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { SafeHtml } from '@angular/platform-browser';
import { NavParams, ModalController } from '@ionic/angular';

@Component({
  selector: 'app-help-page',
  templateUrl: './help-page.component.html',
  styleUrls: ['./help-page.component.scss'],
})
export class HelpPageComponent implements OnInit {
  data: SafeHtml;
  pageName: string;

  constructor(
    private navParams: NavParams,
    public modalCtrl: ModalController,
    private changeDetector: ChangeDetectorRef
  ) {
    this.pageName = this.navParams.get('pageName');
    this.data = this.navParams.get('data');
  }

  async ngOnInit() {
    await this.changeDetector.detectChanges(); // Trigger change detection because html is added to dom dynamically
  }
}
