import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CatchesClient, CatchCreateOrUpdateCommand, GetCatchDetailsCatchItem } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { CatchDetails } from '../models/catch-details.model';


@Injectable({
  providedIn: 'root'
})
export class CatchService {

  constructor(private catchesClient: CatchesClient) { }

  post(command: CatchCreateOrUpdateCommand): Observable<GetCatchDetailsCatchItem> {
    return this.catchesClient.put(command);
  }

  get(id: string): Observable<CatchDetails> {
    return this.catchesClient.get(id)
      .pipe(
        map(response => CatchDetails.fromResponse(response))
      );
  }

  delete = (id: string) => this.catchesClient.delete(id);
}
