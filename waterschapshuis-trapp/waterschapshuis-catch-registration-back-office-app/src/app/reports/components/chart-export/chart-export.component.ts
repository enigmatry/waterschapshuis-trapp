import { Component, Input, OnInit } from '@angular/core';
import { DxChartComponent } from 'devextreme-angular';

@Component({
  selector: 'app-chart-export',
  templateUrl: './chart-export.component.html',
  styleUrls: ['./chart-export.component.scss']
})
export class ChartExportComponent implements OnInit {

  @Input() chart: DxChartComponent;
  @Input() fileName: string;

  constructor() { }

  ngOnInit() {
  }

  print() {
    if (this.chart) {
      this.chart.instance.print();
    }
  }

  exportTo(fileType: string) {
    if (this.chart) {
      this.chart.instance.exportTo(this.fileName, fileType);
    }
  }
}
