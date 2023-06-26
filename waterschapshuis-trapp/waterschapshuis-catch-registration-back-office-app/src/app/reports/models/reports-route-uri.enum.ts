export enum ReportsRouteUri {
  BycatchesReport = 'bycatches-report',
  HourSquareReport = 'hour-square-report',
  OwnReport = 'own-report',
  TrackingLines = 'tracking-lines',
  CatchesByGeoRegion = 'catches-by-geo-region',
  Prediction = 'prediction',
  HeatMap = 'heat-map',
  HourOrganisationReport = 'hour-organisation-report',
  CatchesOrganisationReport = 'catches-organisation-report',
  SubAreaTrackerReport = 'sub-area-tracker-report',
  OrganisationHistogramReport = 'organisation-histogram-report'
}

const standardReportUris = [
    ReportsRouteUri.BycatchesReport,
    ReportsRouteUri.CatchesOrganisationReport,
    ReportsRouteUri.HourOrganisationReport,
    ReportsRouteUri.HourSquareReport,
    ReportsRouteUri.OrganisationHistogramReport,
    ReportsRouteUri.SubAreaTrackerReport
  ];


export {
  standardReportUris
};
