import { parseUrl, stringifyUrl } from 'query-string';

export class LayerFilter {
  layerFullName: string;
  predefinedFilter: string | string[];
  start: Date;
  end: Date;

  constructor(layerFullName: string, predefinedFilter: string | string[], start: Date, end: Date) {
    this.layerFullName = layerFullName;
    this.predefinedFilter = predefinedFilter;
    this.start = start;
    this.end = end;
  }

  static create(layerFullName: string, start: Date, end: Date, predefinedFilter: string | string[]): LayerFilter {
    return new LayerFilter(
      layerFullName,
      predefinedFilter,
      start,
      end
    );
  }


  getFilterValue(): string {
    return `TrackingDate BETWEEN ${this.formatDate(this.start)} AND ${this.formatDate(this.end)}`;
  }

  apply(layerUrl: string): string {
    const url = parseUrl(layerUrl);

    delete url.query.cql_filter;

    if (this.start && this.end) {
      if (url.query.CQL_FILTER) {
        this.predefinedFilter = url.query.CQL_FILTER.toString();
      }

      delete url.query.CQL_FILTER;

      url.query.cql_filter = this.getFilterValue();

    } else {

      if (this.predefinedFilter) {
        url.query.CQL_FILTER = this.predefinedFilter;
      }
    }

    return stringifyUrl(url, { encode: true });
  }

  private formatDate = (date: Date) => {
    const year = date.getFullYear().toString().padStart(4, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
}
