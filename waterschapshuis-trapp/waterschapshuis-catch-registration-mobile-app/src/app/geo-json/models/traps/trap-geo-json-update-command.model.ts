import { GeoJsonUpdateCommand, CommandActionType } from '../geo-json-update-command.model';
import { OverlayLayerName } from '../../../shared/models/overlay-layer-name.enum';
import {
  TrapStatus
} from 'src/app/api/waterschapshuis-catch-registration-mobile-api';
import { TrapCreateOrUpdateCommandVm } from 'src/app/traps/model/trap-create-or-update-command-vm';

export class TrapGeoJsonUpdateCommand extends GeoJsonUpdateCommand {
  constructor(value: TrapCreateOrUpdateCommandVm, status: TrapStatus, sourceMapProjection: string, destinationMapProjection: string) {
    super(
      value.shouldCreate
        ? CommandActionType.Add
        : value.shouldDelete
          ? CommandActionType.Delete
          : CommandActionType.Update,
      value.longitude,
      value.latitude,
      {
        TrapId: value.id,
        SubAreaHourSquareId: null,
        NumberOfTraps: value.numberOfTraps,
        TrapTypeId: value.trapTypeId,
        NumberOfCatches: value.numberOfCatches,
        NumberOfByCatches: value.numberOfByCatches,
        TrapCreatedYear: (new Date()).getFullYear(),
        Status: value.status,
        bbox: []
      },
      'TrapId',
      OverlayLayerName.TrapDetails,
      status === TrapStatus.Catching
        ? 'Active'
        : value.status === TrapStatus.NotCatching ? 'Inactive' : 'Removed',
      new Date(),
      sourceMapProjection,
      destinationMapProjection
    );
  }
}
