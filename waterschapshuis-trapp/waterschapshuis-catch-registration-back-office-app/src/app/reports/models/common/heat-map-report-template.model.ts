import { ReportTemplateBase } from './report-template-base.model';
import { IGetReportTemplatesResponseItem } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class HeatMapReportTemplate extends ReportTemplateBase {
    constructor(
        public title: string,
        public group: string,
        public routeUri: string,
        public exported: boolean,
        public filter: HeatMapReportFilter
    ) {
        super(title, group, routeUri, exported);
    }

    static fromResponse =
        (response: IGetReportTemplatesResponseItem): HeatMapReportTemplate => {
            const content = JSON.parse(response.content);
            return new HeatMapReportTemplate(
                response.title,
                response.group,
                response.routeUri,
                response.exported,
                HeatMapReportFilter.fromContent(content)
            );
        }
}

export class HeatMapReportFilter {
    constructor(
        public startDate: string,
        public endDate: string,
        public isBeverrat: string,
        public organizationId: string) { }

    static fromContent(content: any): HeatMapReportFilter {
        return new HeatMapReportFilter(
            content.fromDate,
            content.toDate,
            content.catchType,
            content.organizationId);
    }

}
