import { Observable } from 'rxjs';
export interface CachedClient {
    cacheAllWithin(boundingBox: BoundingBox): Observable<any>;
}

export interface CachedService {
    cacheAllWithin(boundingBox: BoundingBox): Observable<any>;
    save<T>(value: T): Observable<T>;
}

export interface BoundingBox {
    location: Location;
    widthKilometers: number;
}

export interface Location {
    longitude: number;
    latitude: number;
}


