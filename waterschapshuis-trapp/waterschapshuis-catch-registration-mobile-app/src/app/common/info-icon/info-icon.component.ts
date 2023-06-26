import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { HelpPageComponent } from '../help-page/help-page.component';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-info-icon',
  templateUrl: './info-icon.component.html',
  styleUrls: ['./info-icon.component.scss'],
})
export class InfoIconComponent implements OnInit {
  @Input() pageName: string;

  constructor(
    private modalCtrl: ModalController,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit() {}

  async onInfoIconClick() {
    const helpPageHtml = await fetch('../../assets/help/help.html').then(html => html.text());
    const safeHtml = this.sanitizer.bypassSecurityTrustHtml(helpPageHtml);

    this.modalCtrl.create({
      component: HelpPageComponent,
      cssClass: 'help-modal',
      componentProps: {
        pageName: this.pageName,
        data: safeHtml
      }
    }).then(modal => {
      modal.present().then(() => {
        const page = modal.querySelector(`ion-content`);
        const section: HTMLElement = modal.querySelector(`#${this.pageName}`);
        page.scrollByPoint(0, section.offsetTop, 500);
      });
    });
  }
}
