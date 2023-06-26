import { Guid } from 'guid-typescript';
import {
    IGetCatchDetailsCatchItem,
    IGetCatchTypesResponseItem,
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { CatchCreateOrUpdateCommandVm } from 'src/app/traps/model/catch-create-or-update-command-vm';

export class CatchDetails {
    id: string;
    type: string;
    number: number;
    isByCatch: boolean;
    createdBy: string;
    recordedOn: Date;
    catchTypeId: string;
    // used only for catch removal logic:
    createdOn: Date | null;
    createdById: string | null;
    markedForRemoval: boolean;

    constructor(
        id: string,
        type: string,
        numberOfCatches: number,
        isByCatch: boolean,
        createdBy: string,
        recordedOn: Date,
        catchTypeId: string,
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
            response.createdOn,
            response.createdById
        );
    }

    static fromCatchTypeAndNumber(catchType: IGetCatchTypesResponseItem, numberOfCatches: number) {
        return new CatchDetails(
            Guid.create().toString(),
            catchType.name,
            Number(numberOfCatches),
            catchType.isByCatch,
            undefined,
            new Date(),
            catchType.id
        );
    }

    static fromFormValues(catchType: IGetCatchTypesResponseItem, numberOfCatches: number, recordedOn: Date): CatchDetails {
        return new CatchDetails(
            Guid.create().toString(),
            catchType.name,
            Number(numberOfCatches),
            catchType.isByCatch,
            undefined, // will be set on backend
            recordedOn,
            catchType.id
        );
    }

    toCommand(trapId: string): CatchCreateOrUpdateCommandVm {
        const command = new CatchCreateOrUpdateCommandVm();
        command.id = this.id;
        command.recordedOn = this.recordedOn;
        command.number = this.number;
        command.trapId = trapId;
        command.catchTypeId = this.catchTypeId;
        command.type = this.type;
        command.isByCatch = this.isByCatch;
        command.markedForRemoval = this.markedForRemoval;

        return command;
    }
}
