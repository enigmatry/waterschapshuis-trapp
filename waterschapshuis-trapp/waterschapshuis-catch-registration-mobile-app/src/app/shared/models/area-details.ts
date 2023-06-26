import { ListItem } from './list-item';
import { GetLocationAreaDetailsResponse } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';

export interface IAreaDetails {
    subAreaHourSquareId?: string;
    hourSquare?: ListItem;
    subArea?: ListItem;
    catchArea?: ListItem;
    rayon?: ListItem;
    organization?: ListItem;
}

export class AreaDetails implements IAreaDetails {
    subAreaHourSquareId?: string;
    hourSquare?: ListItem;
    subArea?: ListItem;
    catchArea?: ListItem;
    rayon?: ListItem;
    organization?: ListItem;

    static fromResponse(response: GetLocationAreaDetailsResponse): AreaDetails {
        const result = new AreaDetails();
        result.subAreaHourSquareId = response.subAreaHourSquareId;
        result.hourSquare = ListItem.fromNameEntityItem(response.hourSquare);
        result.subArea = ListItem.fromNameEntityItem(response.subArea);
        result.catchArea = ListItem.fromNameEntityItem(response.catchArea);
        result.rayon = ListItem.fromNameEntityItem(response.rayon);
        result.organization = ListItem.fromNameEntityItem(response.organization);

        return result;
    }
}
