import { ReportTemplateBase } from './report-template-base.model';
import { IGetReportTemplatesResponseItem } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class TrackingLinesReportTemplate extends ReportTemplateBase {
  constructor(
    public title: string,
    public group: string,
    public routeUri: string,
    public exported: boolean,
    public layer: string
  ) {
    super(title, group, routeUri, exported);
  }


  static fromResponse = (response: IGetReportTemplatesResponseItem): TrackingLinesReportTemplate => {
    const content = JSON.parse(response.content);
    return new TrackingLinesReportTemplate(
      response.title,
      response.group,
      response.routeUri,
      response.exported,
      content.layer
    );
  }
}
