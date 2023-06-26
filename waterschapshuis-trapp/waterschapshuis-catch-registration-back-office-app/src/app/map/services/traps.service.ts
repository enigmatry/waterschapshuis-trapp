import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { TrapsClient, IGetTrapDetailsTrapItem, TrapUpdateCommand } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { TrapDetails } from '../models/trap-details.model';

@Injectable({
  providedIn: 'root'
})
export class TrapsService {

  constructor(private trapsClient: TrapsClient) { }

  private movingTrapSubject: BehaviorSubject<boolean> =
    new BehaviorSubject(false);

  movingTrapInProgress$: Observable<boolean> = this.movingTrapSubject.asObservable();

  setMovingTrapInProgress(flag: boolean) {
    this.movingTrapSubject.next(flag);
  }

  get(id: string): Observable<TrapDetails> {
    return this.trapsClient.get(id)
      .pipe(
        map(response => TrapDetails.fromResponse(response))
      );
  }

  getTraps(ids: Array<string>): Observable<Array<TrapDetails>> {
    return ids && ids.length > 0 ? this.trapsClient.getMultiple(ids)
      .pipe(
        map((response: IGetTrapDetailsTrapItem[]) =>
          response.map(trap => TrapDetails.fromResponse(trap))
        )
      ) : of([]);
  }

  post(command: TrapUpdateCommand): Observable<IGetTrapDetailsTrapItem> {
    return this.trapsClient.put(command);
  }

  deleteTrap(trapId: string): Observable<boolean> {
    return this.trapsClient.delete(trapId);
  }
}
