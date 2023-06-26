import { IGetCatchDetailsCatchItem } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class CatchDetails {
    id: string;
    type: string;
    number: number;
    isByCatch: boolean;
    createdBy: string;
    recordedOn: Date;
    catchTypeId: string;
    canBeEdited: boolean;
    // used only for catch removal logic:
    createdOn: Date | null;
    createdById: string | null;

    constructor(
        id: string,
        type: string,
        numberOfCatches: number,
        isByCatch: boolean,
        createdBy: string,
        recordedOn: Date,
        catchTypeId: string,
        canBeEdited: boolean,
        createdOn: Date = null,
        createdById: string = null
    ) {
        this.id = id;
        this.type = type;
        this.number = numberOfCatches;
        this.isByCatch = isByCatch;
        this.createdBy = createdBy;
        this.recordedOn = new Date(recordedOn);
        this.catchTypeId = catchTypeId;
        this.canBeEdited = canBeEdited;
        this.createdOn = createdOn ? new Date(createdOn) : createdOn;
        this.createdById = createdById;
    }

    static fromResponse(response: IGetCatchDetailsCatchItem): CatchDetails {
        return new CatchDetails(
            response.id,
            response.type,
            response.number,
            response.isByCatch,
            response.createdBy,
            response.recordedOn,
            response.catchTypeId,
            response.canBeEdited,
            response.createdOn,
            response.createdById
        );
    }
}
