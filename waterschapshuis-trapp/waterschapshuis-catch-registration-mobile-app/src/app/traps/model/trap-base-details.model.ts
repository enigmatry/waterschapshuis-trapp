
import { INamedEntityItem, TrapStatus } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { TrapDetails } from 'src/app/maps/models/trap-details.model';
import { Guid } from 'guid-typescript';

export class TrapBaseDetails {
  id: string;
  status: TrapStatus;
  remarks?: string;
  numberOfTraps: number;
  createdOn: Date;
  recordedOn: Date;
  trapType: INamedEntityItem;
  trappingTypeId: string;
  numberOfCatches: number;

  isEditAllowed: boolean;

  constructor(
    id: string,
    trapType: INamedEntityItem,
    trappingTypeId: string,
    status: TrapStatus,
    remarks: string,
    numberOfTraps: number,
    createdOn: Date,
    recordedOn: Date,
    numberOfCatches: number,
    isEditAllowed: boolean
  ) {
    this.id = id;
    this.trapType = trapType;
    this.trappingTypeId = trappingTypeId;
    this.status = status;
    this.remarks = remarks;
    this.numberOfTraps = numberOfTraps;
    this.createdOn = createdOn;
    this.recordedOn = recordedOn;
    this.numberOfCatches = numberOfCatches;
    this.isEditAllowed = isEditAllowed;

  }

  static fromTrapDetails(trapDetails: TrapDetails): TrapBaseDetails {
    return new TrapBaseDetails(
      trapDetails.id,
      { id: trapDetails.trapTypeId, name: trapDetails.type },
      trapDetails.trappingTypeId,
      trapDetails.status,
      trapDetails.remarks,
      trapDetails.numberOfTraps,
      trapDetails.createdOn,
      trapDetails.recordedOn,
      trapDetails.catches?.length,
      trapDetails.isEditAllowed
    );
  }

  static getDefault(trappingTypeId: string, trapType: INamedEntityItem): TrapBaseDetails {
    return new TrapBaseDetails(
      Guid.create().toString(),
      trapType,
      trappingTypeId,
      TrapStatus.Catching,
      undefined,
      1,
      new Date(),
      new Date(),
      0,
      false
    );
  }
}
