import { GetFieldTestResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { ListItem } from 'src/app/shared/models/list-item';

export class FieldTest {
    id?: string;
    name?: string;
    startPeriod?: string;
    endPeriod?: string;
    hourSquares?: ListItem[];

    constructor(
        id: string,
        name: string,
        startPeriod: string,
        endPeriod: string,
        hourSquares: ListItem[]
    ) {
        this.id = id;
        this.name = name;
        this.startPeriod = startPeriod;
        this.endPeriod = endPeriod;
        this.hourSquares = hourSquares;
    }


    static fromResponse(response: GetFieldTestResponse): FieldTest {
        return new FieldTest(
            response.id,
            response.name,
            response.startPeriod,
            response.endPeriod,
            ListItem.mapToListItems(response.hourSquares)
        );
    }
}
