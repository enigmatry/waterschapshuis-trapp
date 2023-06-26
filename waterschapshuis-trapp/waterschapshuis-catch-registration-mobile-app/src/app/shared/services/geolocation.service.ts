import { Injectable } from '@angular/core';
import { Geolocation, Geoposition } from '@ionic-native/geolocation/ngx';
import { Coordinate } from 'ol/coordinate';
import { Extent } from 'ol/extent';
import { ProjectionLike, transform } from 'ol/proj';
import { from, Observable, throwError, BehaviorSubject, of } from 'rxjs';
import { catchError, map, switchMap, filter } from 'rxjs/operators';
import { ProjectionModel } from 'src/app/maps/models/projection.model';
import { AppSettings } from 'src/app/app-configuration/app-settings';
import { Logger } from 'src/app/core/logger/logger';
import { extentFromCoordinate } from 'src/app/maps/models/extent.extensions';

const logger = new Logger('GeolocationService');

@Injectable({
  providedIn: 'root'
})
export class GeolocationService {

  private lastKnownPositionSubject = new BehaviorSubject<Geoposition>(null);

  private readonly options = {
    timeout: 7000,
    enableHighAccuracy: true,
    maximumAge: 2500
  };

  constructor(private geolocation: Geolocation) {
    ProjectionModel.initDutchProjection();
  }

  watch$: Observable<Geoposition> =
    this.geolocation.watchPosition(this.options).pipe(filter(p => p.coords !== undefined));

  get lastKnownPosition(): Geoposition {
    return this.lastKnownPositionSubject.value;
  }

  showLocationConsentPrompt() {
    return this.getCurrentPosition()
      .pipe(switchMap(() => this.watch$))
      .subscribe(position => this.lastKnownPositionSubject.next(position));
  }

  watchAs(projection: ProjectionLike): Observable<Coordinate> {
    return this.watch$.pipe(
      map(position => this.transform(position, projection)),
      catchError(_ => this.watchAs(projection))
    );
  }

  getCurrentPosition(): Observable<Geoposition> {
    return from(this.geolocation.getCurrentPosition(this.options))
      .pipe(catchError(error => {
        return this.lastKnownPosition ? of(this.lastKnownPosition) : throwError(error);
      }));
  }

  getCurrentPositionAs(projection: ProjectionLike): Observable<Coordinate> {
    return this.getCurrentPosition()
      .pipe(map(position => this.transform(position, projection)));
  }

  getCurrentCacheableExtent(): Observable<Extent> {
    const projection = ProjectionModel.dutchMatrix;
    return this.getExtentFromCurrentPosition(projection, AppSettings.prefetchDataSettings.widthKilometers * 1000);
  }

  private getExtentFromCurrentPosition(projection: ProjectionLike, radius: number): Observable<Extent> {
    return this.getCurrentPositionAs(projection)
      .pipe(map(position => extentFromCoordinate(position, radius)));
  }

  private transform(position: Geoposition, projection: ProjectionLike) {
    return transform([position.coords.longitude, position.coords.latitude], ProjectionModel.geodeticMatrix, projection);
  }
}
