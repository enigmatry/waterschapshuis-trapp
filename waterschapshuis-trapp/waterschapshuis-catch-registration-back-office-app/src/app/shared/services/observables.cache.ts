import { Observable } from 'rxjs';
import { shareReplay, takeUntil } from 'rxjs/operators';

export class ObservablesCache {
    private CacheSize = 1; // see https://blog.thoughtram.io/angular/2018/03/05/advanced-caching-with-rxjs.html
    private caches: { [cacheName: string]: Observable<any> } = {};

    public getFromCache<T>(cacheName: string, fetch: () => Observable<T>, removeFromCache$: Observable<any> = null) {
        const cachedObservable = this.caches[cacheName];
        if (cachedObservable) {
            return cachedObservable as Observable<T>;
        } else {
            let observable = fetch();
            if (removeFromCache$) {
                observable = observable.pipe(takeUntil(removeFromCache$));
            }
            observable = observable.pipe(shareReplay(this.CacheSize));
            this.caches[cacheName] = observable;
            return observable;
        }
    }

    public clear() {
        this.caches = {};
    }
}
