import {
    CatchStatus,
    IGetTimeRegistrationsOfWeekResponseCatchItem,
    TimeRegistrationsEditCatchItem,
} from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { IListItem } from 'src/app/shared/models/list-item';

export class CatchRegistration {
    id: string;
    recordedOn: Date;
    catchArea?: IListItem = null;
    subArea?: IListItem = null;
    hourSquare?: IListItem = null;
    number: number;
    status: CatchStatus;
    isByCatch: boolean;
    catchTypeId: string;

    constructor() { }

    static fromResponse(response: IGetTimeRegistrationsOfWeekResponseCatchItem): CatchRegistration {
        const catchRegItem = new CatchRegistration();
        catchRegItem.id = response.id;
        catchRegItem.recordedOn = response.recordedOn;
        catchRegItem.catchArea = response.catchArea;
        catchRegItem.subArea = response.subArea;
        catchRegItem.hourSquare = response.hourSquare;
        catchRegItem.number = response.number;
        catchRegItem.status = response.status;
        catchRegItem.isByCatch = response.isByCatch;
        catchRegItem.catchTypeId = response.catchTypeId;

        return catchRegItem;
    }

    static toCommand(catches: CatchRegistration[], status?: CatchStatus): TimeRegistrationsEditCatchItem[] {
        const commandItems: TimeRegistrationsEditCatchItem[] = [];

        catches.forEach(item => {
            if (item) {
                commandItems.push(new TimeRegistrationsEditCatchItem({
                    id: item.id,
                    catchTypeId: item.catchTypeId,
                    number: item.number,
                    status: status ?? item.status,
                    isByCatch: item.isByCatch
                }));
            }
        });
        return commandItems;
    }
}
