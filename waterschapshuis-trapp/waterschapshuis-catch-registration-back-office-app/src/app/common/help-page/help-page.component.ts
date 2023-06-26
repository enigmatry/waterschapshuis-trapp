import { Component, OnInit, Inject, ViewChild, ChangeDetectorRef, ElementRef } from '@angular/core';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-help-page',
  templateUrl: './help-page.component.html',
  styleUrls: ['./help-page.component.scss']
})
export class HelpPageComponent implements OnInit {
  safeHtml: SafeHtml;

  @ViewChild('helpModalBody', { read: ElementRef }) modalBody: ElementRef;

  constructor(
    private sanitizer: DomSanitizer,
    private changeDetector: ChangeDetectorRef,
    public dialogRef: MatDialogRef<HelpPageComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  async ngOnInit() {
    const helpPageHtml = await fetch('../../../assets/help/help.html').then(html => html.text());
    this.safeHtml = this.sanitizer.bypassSecurityTrustHtml(helpPageHtml);

    this.changeDetector.detectChanges(); // Trigger change detection because html is added to dom dynamically

    const el = this.modalBody.nativeElement.querySelector(`#${this.data.pageName}`);
    if (el) {
      el.scrollIntoView();
    }
  }
}
