import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { SearchMapModel } from '../models/search-map.model';
import { MapSearchParams } from '../models/search-map-param.model';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FindService {
  private searchDataParameter: Subject<MapSearchParams> = new Subject();
  private previewItem: Subject<SearchMapModel> = new Subject();

  constructor(
    private httpClient: HttpClient
  ) {}

  updateSearchParams(parameters: MapSearchParams): void {
    this.searchDataParameter.next(parameters);
  }

  public onSearchParamsChange(): Observable<MapSearchParams> {
    return this.searchDataParameter.asObservable();
  }

  getSearchedDataPreview(parameters) {
    return this.httpClient.get<SearchMapModel>(`${environment.searchMap.url}suggest?q=${parameters.value}
      &lat=${parameters.latitude}&lon=${parameters.longitude}&rows=15&start=${parameters.startIndex}`);
  }

  getDataForSelectedPreviewItem(item) {
    return this.httpClient.get<SearchMapModel>(`${environment.searchMap.url}lookup?id=${item.id}`);
  }
}
