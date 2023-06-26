import { ReportTemplateBase } from './report-template-base.model';
import { IGetReportTemplatesResponseItem } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class CatchesByGeoRegionReportTemplate extends ReportTemplateBase {
    constructor(
        public title: string,
        public group: string,
        public routeUri: string,
        public exported: boolean,
        public filter: CatchesByGeoRegionReportFilter
    ) {
        super(title, group, routeUri, exported);
    }

    static fromResponse =
        (response: IGetReportTemplatesResponseItem): CatchesByGeoRegionReportTemplate => {
            const content = JSON.parse(response.content);
            return new CatchesByGeoRegionReportTemplate(
                response.title,
                response.group,
                response.routeUri,
                response.exported,
                CatchesByGeoRegionReportFilter.fromContent(content)
            );
        }
}

export class CatchesByGeoRegionReportFilter {
    constructor(
        public layer: string,
        public measurement: number,
        public versionRegionalLayout: string,
        public yearFrom: number,
        public periodFrom: number,
        public yearTo: number,
        public periodTo: number,
        public trappingType: string) { }

    static fromContent(content: any): CatchesByGeoRegionReportFilter {
        return new CatchesByGeoRegionReportFilter(
            content.layer,
            content.measurement,
            content.versionRegionalLayout,
            content.yearFrom,
            content.periodFrom,
            content.yearTo,
            content.periodTo,
            content.trappingType);
    }
}
