import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import PivotGridDataSource from 'devextreme/ui/pivot_grid/data_source';
import { IGetReportTemplatesResponseItem, ReportTemplateType, ChartType } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/core/auth/auth.service';
import { ReportTemplateBase } from './report-template-base.model';
import { DxFunctions } from '../dev-extreme-functions.model';
import { YearAndPeriodHelper } from '../year-and-period-helper.model';
import { LookupsService } from 'src/app/shared/services/lookups.service';
import { isString } from 'src/app/shared/models/utils';

export class DevExtremeReportTemplate extends ReportTemplateBase {
    constructor(
        public title: string,
        public group: string,
        public routeUri: string,
        public exported: boolean,
        public type: ReportTemplateType,
        public key: string,
        public exportFileName: string,
        public fields: any[],
        public chartType: ChartType
    ) {
        super(title, group, routeUri, exported);
    }

    static fromResponse =
        (response: IGetReportTemplatesResponseItem): DevExtremeReportTemplate =>
            new DevExtremeReportTemplate(
                response.title,
                response.group,
                response.routeUri,
                response.exported,
                response.type,
                response.key,
                response.exportFileName,
                JSON.parse(response.content),
                response.chartType
            )

    createDataSource = (authService: AuthService, lookupsService: LookupsService): Observable<PivotGridDataSource> =>
        authService
            .getAccessToken()
            .pipe(
                map(accessToken => new PivotGridDataSource({
                    remoteOperations: true,
                    fields: this.applyCustomValuesToDxDataSource(this.fields, lookupsService),
                    store: AspNetData.createStore({
                        key: this.key,
                        loadUrl: `${environment.apiUrl}/Reports?reportUri=${this.routeUri}`,
                        // MsalInterceptor logic does not work here because DevExtreme component doesn't use HttpClient nor HttpInterceptor!
                        // tslint:disable-next-line: max-line-length
                        // https://supportcenter.devexpress.com/Ticket/Details/T813411/odata-store-with-httpinterceptor-for-authentication-via-msal
                        onBeforeSend: (operation, request) => request.headers = { Authorization: `Bearer ${accessToken}` }
                    })
                }))
            )

    createReportFileName = (): string => `${this.exportFileName} (${new Date().toDateString()})`;

    private applyCustomValuesToDxDataSource = (fields: any[], lookupsService: LookupsService): any[] => {
        fields.forEach(async field => {
            if (field.dxFunctionPlaceholder) {
                field.calculateSummaryValue =
                    DxFunctions.tryGetFunctionImplementation(field.dxFunctionPlaceholder);
            } else if (field.filterValues === undefined) {
                if (field.dataField === 'versionRegionalLayout') {
                    const vrls = await lookupsService.getVersionRegionalLayouts().pipe(take(1)).toPromise();
                    field.filterValues = vrls ? [vrls[0].name, null] : [null];
                } else if (field.dataField === 'yearAndPeriod') {
                    field.filterValues =
                        YearAndPeriodHelper.getFilterValuesForYearAndPeriod();
                } else if (field.dataField === 'recordedOnYear') {
                    field.filterValues =
                        [new Date().getFullYear(), null];
                }
            }
            if (field.sortingMethod && isString(field.sortingMethod)) {
                field.sortingMethod = DxFunctions[field.sortingMethod];
            }
        });
        return fields;
    }
}
