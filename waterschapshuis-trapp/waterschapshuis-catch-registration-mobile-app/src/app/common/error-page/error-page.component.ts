import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.scss']
})
export class ErrorPageComponent implements OnInit {

  private pageDataByType: { [type: string]: { title: string, message: string } } = {
    401: { title: 'Not Authorized', message: 'U bent niet geautoriseerd voor deze applicatie.' },
    403: { title: 'Forbidden', message: 'U heeft geen toegang tot deze applicatie.' },
    404: { title: '404 Not Found', message: 'Pagina kan niet gevonden worden.' },
    500: { title: 'Error', message: 'Er is een fout opgetreden.' },
  };

  title: string;
  message: string;

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params
      .subscribe(
        (data: Params) => {
          this.title = this.pageDataByType[data.type] ? this.pageDataByType[data.type].title : this.pageDataByType[500].title;
          this.message = this.pageDataByType[data.type] ? this.pageDataByType[data.type].message : this.pageDataByType[500].message;
        });
  }
}
