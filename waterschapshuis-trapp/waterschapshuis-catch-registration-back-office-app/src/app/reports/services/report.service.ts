import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import {
    IGetReportTemplatesResponse,
    IGetReportTemplatesResponseItem,
    ReportsClient,
    ReportTemplateType,
    CreateReportTemplateExportCommand,
    CreateReportTemplateExportResult,
    ChartType
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

import { CatchesByGeoRegionReportTemplate } from '../models/common/catches-by-geo-region-report-template.model';
import { ReportTemplateBase } from '../models/common/report-template-base.model';
import { TrackingLinesReportTemplate } from '../models/common/tracking-lines-report-template.model';
import { DevExtremeReportTemplate } from '../models/common/dev-extreme-report-template.model';
import { ReportsRouteUri } from '../models/reports-route-uri.enum';
import { PredictionRequest } from '../models/prediction-request.model';
import { HeatMapReportTemplate } from '../models/common/heat-map-report-template.model';

@Injectable({
    providedIn: 'root'
})
export class ReportService {

    private reportTemplates: Array<ReportTemplateBase>;

    constructor(private reportClient: ReportsClient) { }

    getReportTemplateByUri = async (reportRouteUri: string): Promise<ReportTemplateBase> =>
        (await this.getReportTemplates()).find(x => x.routeUri === reportRouteUri)

    getReportTemplatesByGroup = async (group: ReportTemplateType): Promise<DevExtremeReportTemplate[]> =>
        (await this.getReportTemplates() as DevExtremeReportTemplate[]).filter(x => x.type === group)

    getReportTemplateById = async (id: string): Promise<ReportTemplateBase> =>
        this.reportClient
            .getReportTemplate(id)
            .pipe(
                map(x => x.item),
                map(x => this.mapTemplateResponse(x))
            ).toPromise()

    createReportTemplateExport = async (
        originReportUri: string,
        newTemplateTitle: string,
        newTemplateContent: string,
        newChartType: ChartType)
        : Promise<CreateReportTemplateExportResult> =>
            this.reportClient
                .createReportTemplate(CreateReportTemplateExportCommand.fromJS({
                    reportUri: originReportUri,
                    templateTitle: newTemplateTitle,
                    templateContent: newTemplateContent,
                    chartType: newChartType
                }))
                .toPromise()

    getPrediction = (request: PredictionRequest) =>
        this.reportClient.getPrediction(
            request.hourSquareId,
            request.summerCatches,
            request.springCatches,
            request.autumnCatches,
            request.winterCatches,
            request.summerHours,
            request.springHours,
            request.autumnHours,
            request.winterHours)

    private getReportTemplates = async (): Promise<Array<ReportTemplateBase>> => {
        if (!this.reportTemplates) {
            this.reportTemplates = await this.loadReportTemplates();
        }
        return this.reportTemplates;
    }

    private loadReportTemplates = async (): Promise<Array<ReportTemplateBase>> =>
        this.reportClient
            .getReportTemplates()
            .pipe(
                map((data: IGetReportTemplatesResponse) =>
                    data.items.map((responseItem: IGetReportTemplatesResponseItem) => this.mapTemplateResponse(responseItem))
                )
            ).toPromise()


    private mapTemplateResponse = (response: IGetReportTemplatesResponseItem): ReportTemplateBase => {
        if (response.type === ReportTemplateType.DevExtreme || response.type === ReportTemplateType.StandardReport) {
            return DevExtremeReportTemplate.fromResponse(response);
        }
        switch (response.routeUri) {
            case ReportsRouteUri.TrackingLines: return TrackingLinesReportTemplate.fromResponse(response);
            case ReportsRouteUri.CatchesByGeoRegion: return CatchesByGeoRegionReportTemplate.fromResponse(response);
            case ReportsRouteUri.HeatMap: return HeatMapReportTemplate.fromResponse(response);
            default: throw new Error('Report template cannot be mapped!');
        }
    }

}
