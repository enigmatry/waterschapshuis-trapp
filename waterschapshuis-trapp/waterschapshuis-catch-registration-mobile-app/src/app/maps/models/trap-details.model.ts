import { IGetTrapDetailsTrapItem, IGetCatchDetailsCatchItem, TrapStatus } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { CatchDetails } from './catch-details.model';
import { TrapStatusNL } from './trap-status.enum';
import { TrapCreateOrUpdateCommandVm } from 'src/app/traps/model/trap-create-or-update-command-vm';


export class TrapDetails {

  id: string;
  type: string;
  status: TrapStatus;
  remarks?: string;
  numberOfTraps: number;
  createdBy: string;
  createdById: string;
  createdOn: Date;
  recordedOn: Date;
  catches: Array<CatchDetails> = [];
  trapTypeId: string;
  trappingTypeId: string;
  longitude: number;
  latitude: number;
  updatedBy: string;
  updatedOn: Date;
  rayon: string;
  subArea: string;
  catchArea: string;

  canUpdate: boolean;
  canUpdateDetails: boolean;

  public get numberOfCatches(): number {
    return this.catchesOnly.reduce((sum, current) => sum + current.number, 0);
  }

  public get numberOfByCatches(): number {
    return this.byCatchesOnly.reduce((sum, current) => sum + current.number, 0);
  }

  public get catchesOnly(): Array<CatchDetails> {
    return this.catches.filter(x => !x.isByCatch);
  }

  public get byCatchesOnly(): Array<CatchDetails> {
    return this.catches.filter(x => x.isByCatch);
  }

  public get statusTitle(): string {
    return TrapStatusNL[this.status];
  }

  public get isEditAllowed(): boolean {
    return this.createdOn.toDateString() === new Date().toDateString() && this.numberOfCatches === 0;
  }

  constructor(
    id: string,
    type: string,
    trapTypeId: string,
    trappingTypeId: string,
    status: TrapStatus,
    remarks: string,
    numberOfTraps: number,
    createdBy: string,
    createdOn: Date,
    recordedOn: Date,
    catches: Array<IGetCatchDetailsCatchItem>,
    longitude: number,
    latitude: number,
    updatedBy: string,
    updatedOn: Date,
    rayon: string,
    subArea: string,
    catchArea: string,
    createdById: string
  ) {
    this.id = id;
    this.type = type;
    this.trapTypeId = trapTypeId;
    this.trappingTypeId = trappingTypeId;
    this.status = status;
    this.remarks = remarks;
    this.numberOfTraps = numberOfTraps;
    this.createdBy = createdBy;
    this.createdById = createdById;
    this.createdOn = createdOn;
    this.recordedOn = recordedOn;
    this.longitude = longitude;
    this.latitude = latitude;
    this.updatedBy = updatedBy;
    this.updatedOn = updatedOn;
    this.rayon = rayon;
    this.subArea = subArea;
    this.catchArea = catchArea;

    this.catches = catches && catches.length > 0 ? catches.map(x => CatchDetails.fromResponse(x)) : [];
  }

  static fromResponse(response: IGetTrapDetailsTrapItem): TrapDetails {
    return new TrapDetails(
      response.id,
      response.type,
      response.trapTypeId,
      response.trappingTypeId,
      response.status,
      response.remarks,
      response.numberOfTraps,
      response.createdBy,
      new Date(response.createdOn),
      new Date(response.recordedOn),
      response.catches,
      response.longitude,
      response.latitude,
      response.updatedBy,
      response.updatedOn,
      response.rayon,
      response.subArea,
      response.catchArea,
      response.createdById
    );
  }

  static createCommand(
    id: string,
    trapTypeId: string,
    status: TrapStatus,
    remarks: string,
    numberOfTraps: number,
    recordedOn: Date,
    catches: Array<CatchDetails>,
    longitude: number,
    latitude: number,
    previousStatus: TrapStatus,
    shouldCreate: boolean,
    numberOfCatches: number,
    numberOfByCatches: number,
    createdOn: Date,
    createdBy: string
  ): TrapCreateOrUpdateCommandVm {

    const command = new TrapCreateOrUpdateCommandVm();

    command.id = id;
    command.recordedOn = recordedOn;
    command.numberOfTraps = numberOfTraps;
    command.status = status;
    command.longitude = longitude;
    command.latitude = latitude;
    command.trapTypeId = trapTypeId;
    command.remarks = remarks;
    command.catches = catches.map(c => c.toCommand(id));
    command.shouldCreate = shouldCreate;
    command.previousStatus = previousStatus;
    command.numberOfCatches = numberOfCatches;
    command.numberOfByCatches = numberOfByCatches;
    command.createdOn = createdOn;
    command.createdBy = createdBy;

    return command;
  }
}
