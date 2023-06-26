import { IGetTrapDetailsTrapItem, TrapStatus, IGetCatchDetailsCatchItem } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { CatchDetails } from './catch-details.model';
import { TrapStatusNL } from './trap-status.enum';

export class TrapDetails {
  id: string;
  type: string;
  status: TrapStatus;
  remarks?: string;
  numberOfTraps: number;
  createdBy: string;
  createdOn: Date;
  recordedOn: Date;
  catches: Array<CatchDetails> = [];
  trappingTypeId: string;
  trapTypeId: string;
  longitude: number;
  latitude: number;

  public get numberOfCatches(): number {
    return this.catchesOnly.reduce((sum, current) => sum + current.number, 0);
  }

  public get numberOfByCatches(): number {
    return this.byCatchesOnly.reduce((sum, current) => sum + current.number, 0);
  }

  public get catchesOnly(): Array<CatchDetails> {
    return this.catches.filter(x => !x.isByCatch).sort((a, b) => b.recordedOn.getTime() - a.recordedOn.getTime());
  }

  public get byCatchesOnly(): Array<CatchDetails> {
    return this.catches.filter(x => x.isByCatch).sort((a, b) => b.recordedOn.getTime() - a.recordedOn.getTime());
  }

  public get statusTitle(): string {
    return TrapStatusNL[this.status];
  }

  constructor(
    id: string,
    type: string,
    status: TrapStatus,
    remarks: string,
    numberOfTraps: number,
    createdBy: string,
    createdOn: Date,
    recordedOn: Date,
    catches: Array<IGetCatchDetailsCatchItem>,
    trappingTypeId: string,
    trapTypeId: string,
    longitude: number,
    latitude: number
  ) {
    this.id = id;
    this.type = type;
    this.status = status;
    this.remarks = remarks;
    this.numberOfTraps = numberOfTraps;
    this.createdBy = createdBy;
    this.createdOn = createdOn;
    this.recordedOn = recordedOn;
    this.trappingTypeId = trappingTypeId;
    this.trapTypeId = trapTypeId;
    this.longitude = longitude;
    this.latitude = latitude;

    this.catches = catches.map(x => CatchDetails.fromResponse(x));
  }

  static fromResponse(response: IGetTrapDetailsTrapItem): TrapDetails {
    return new TrapDetails(
      response.id,
      response.type,
      response.status,
      response.remarks,
      response.numberOfTraps,
      response.createdBy,
      response.createdOn,
      response.recordedOn,
      response.catches,
      response.trappingTypeId,
      response.trapTypeId,
      response.longitude,
      response.latitude
    );
  }
}
