import { IGetObservationDetailsResponseItem } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';

export class ObservationResponse {
    id: string;
    type: string;
    createdBy: string;
    image: string;
    photoUrl: string;
    remarks: string;
    createdOn: Date;
    archived: boolean;
    recordedOn: Date;
    longitude: number;
    latitude: number;

    constructor(
        id: string,
        type: number,
        createdBy: string,
        image: string,
        photoUrl: string,
        remarks: string,
        createdOn: Date,
        archived: boolean,
        recordedOn: Date,
        longitude: number,
        latitude: number) {
        this.id = id;
        this.type = type.toString();
        this.createdBy = createdBy;
        this.image = image;
        this.photoUrl = photoUrl;
        this.remarks = remarks;
        this.createdOn = createdOn;
        this.archived = archived;
        this.recordedOn = recordedOn;
        this.longitude = longitude;
        this.latitude = latitude;
    }

    static fromResponse(response: any): any {
        return new ObservationResponse(
            response.id,
            response.type,
            response.createdBy,
            response.image,
            response.photoUrl,
            response.remarks,
            response.createdOn,
            response.archived,
            response.recordedOn,
            response.longitude,
            response.latitude
        );
    }
}
