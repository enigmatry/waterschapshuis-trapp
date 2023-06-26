import { GetObservationDetailsResponseItem } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class ObservationDetails {
  id?: string;
  createdBy?: string;
  type?: number;
  photoUrl?: string;
  remarks?: string;
  createdOn?: Date;
  position?: string;
  archived?: boolean;
  recordedOn?: Date;
  longitude?: number;
  latitude?: number;

  constructor(
    id: string,
    createdBy: string,
    type: number,
    photoUrl: string,
    remarks: string,
    createdOn: Date,
    position: string,
    archived: boolean,
    recordedOn: Date,
    longitude: number,
    latitude: number) {
    this.id = id;
    this.createdBy = createdBy;
    this.type = type;
    this.photoUrl = photoUrl;
    this.remarks = remarks;
    this.createdOn = createdOn;
    this.position = position;
    this.archived = archived;
    this.recordedOn = recordedOn;
    this.longitude = longitude;
    this.latitude = latitude;
  }

  static fromResponse(response: GetObservationDetailsResponseItem) {
    return new ObservationDetails(
      response.id,
      response.createdBy,
      response.type,
      response.photoUrl,
      response.remarks,
      response.createdOn,
      response.position,
      response.archived,
      response.recordedOn,
      response.longitude,
      response.latitude
    );
  }

}
