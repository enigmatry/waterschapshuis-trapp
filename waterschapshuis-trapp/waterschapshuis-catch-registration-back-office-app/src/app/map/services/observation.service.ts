/**
 *  Currently not used and disabled, see https://jira.enigmatry.com/browse/WVR-388.
 */
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import {
  GetObservationDetailsResponseItem,
  ObservationsClient,
  ObservationUpdateCommand
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { ObservationDetails } from 'src/app/map/models/observation-details.model';

@Injectable({
  providedIn: 'root'
})
export class ObservationService {

  constructor(private observationClient: ObservationsClient) { }

  getObservations(ids: Array<string>): Observable<Array<ObservationDetails>> {
    return ids && ids.length > 0 ? this.observationClient.getMultiple(ids)
      .pipe(
        map((response: GetObservationDetailsResponseItem[]) =>
          response.map(obs => ObservationDetails.fromResponse(obs))
        )
      ) : of([]);
  }

  archiveActiveObservation(value: ObservationDetails): Observable<GetObservationDetailsResponseItem> {
    const command = ObservationUpdateCommand.fromJS({ ...value, archived: true });
    return this.observationClient.update(command);
  }
}
