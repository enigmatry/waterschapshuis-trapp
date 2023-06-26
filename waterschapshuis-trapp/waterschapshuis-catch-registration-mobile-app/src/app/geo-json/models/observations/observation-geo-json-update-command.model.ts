import { GeoJsonUpdateCommand, CommandActionType } from '../geo-json-update-command.model';
import { Observation } from 'src/app/maps/models/observation.model';
import { OverlayLayerName } from '../../../shared/models/overlay-layer-name.enum';
import { ObservationUpdateCommandVm } from 'src/app/maps/services/observation-update.service';

export class ObservationGeoJsonUpdateCommand extends GeoJsonUpdateCommand {

    constructor() {
        super(
            undefined,
            undefined,
            undefined,
            undefined,
            undefined,
            undefined,
            undefined,
            new Date(),
            undefined,
            undefined);
    }

    static createCommand = (value: Observation, sourceMapProjection: string, destinationMapProjection: string)
        : ObservationGeoJsonUpdateCommand => {
        const result = new ObservationGeoJsonUpdateCommand();

        result.sourceMapProjection = sourceMapProjection;
        result.destinationMapProjection = destinationMapProjection;
        result.longitude = value.longitude;
        result.latitude = value.latitude;
        result.setCoordinates(value.longitude, value.latitude);
        result.actionType = CommandActionType.Add;
        result.properties = {
            Id: value.id,
            SubAreaHourSquareId: undefined,
            Type: Number(value.type),
            Status: undefined,
            Archived: false,
            StyleCode: undefined,
            bbox: []
        };
        result.propertiesIdFieldName = 'Id';
        result.layerName = OverlayLayerName.Observations;
        result.layerNameSuffix = 'Active';

        return result;
    }

    static updateCommand = (
        value: ObservationUpdateCommandVm,
        layerSufix: string, sourceMapProjection: string,
        destinationMapProjection: string
    )
        : ObservationGeoJsonUpdateCommand => {
        const result = new ObservationGeoJsonUpdateCommand();

        result.sourceMapProjection = sourceMapProjection;
        result.destinationMapProjection = destinationMapProjection;
        result.longitude = value.longitude;
        result.latitude = value.latitude;
        result.setCoordinates(value.longitude, value.latitude);
        result.actionType = CommandActionType.Update;
        result.properties = {
            Id: value.id,
            SubAreaHourSquareId: undefined,
            Type: value.type,
            Status: undefined,
            Archived: true,
            StyleCode: undefined,
            bbox: []
        };
        result.propertiesIdFieldName = 'Id';
        result.layerName = OverlayLayerName.Observations;
        result.layerNameSuffix = layerSufix;

        return result;
    }
}
