import { BoundingBox } from 'src/app/cache/clients/cached.client';
import { LayerFilter } from 'src/app/maps/models/layer-filter.model';
import { SqliteMigrations } from 'src/app/services/sqlite-migrations';

export class AppSettings {
    private static Separator = ';';
    public static Id = 'AppSettingId';

    public id = AppSettings.Id;
    public overlayLayerIds = new Array<string>();
    public backgroundLayerId = 'brtachtergrondkaart';
    public offlineBboxJson: string;
    public layerFilterJson: string;
    public lastMigrationId: number;

    public static fromObject = (object: any): AppSettings => {
        const result = new AppSettings();

        if (object.overlayLayerIds) {
            result.overlayLayerIds = object.overlayLayerIds.split(AppSettings.Separator);
        }

        if (object.backgroundLayerId) {
            result.backgroundLayerId = object.backgroundLayerId;
        }

        if (object.offlineBboxJson) {
            result.offlineBboxJson = object.offlineBboxJson;
        }

        if (object.trackingLayerFilterJson && object.trackingLayerFilterJson !== 'undefined') {
            result.layerFilterJson = object.trackingLayerFilterJson;
        }

        if (object.lastMigrationId) {
            result.lastMigrationId = isNaN(object.lastMigrationId) ? 0 : Number(object.lastMigrationId);
        }

        return result;
    }

    get layerFilter(): LayerFilter {
        return (this.layerFilterJson) ? JSON.parse(this.layerFilterJson) : null;
    }

    set layerFilter(value: LayerFilter) {
        this.layerFilterJson = (value) ? JSON.stringify(value) : null;
    }

    get offlineBoundingBox(): BoundingBox {
        return (this.offlineBboxJson) ? JSON.parse(this.offlineBboxJson) : null;
    }

    set offlineBoundingBox(value: BoundingBox) {
        this.offlineBboxJson = JSON.stringify(value);
    }

    public getValues = (): Array<any> => [
        this.id,
        this.overlayLayerIds.join(AppSettings.Separator),
        this.backgroundLayerId,
        this.offlineBboxJson,
        this.layerFilterJson,

        // if user installs new version of application, then lastMigrationId is set to id of the last migration in list
        // to avoid executin migrations if it si not needed
        this.lastMigrationId ? this.lastMigrationId : SqliteMigrations.lastMigrationId
    ]

    public getObject = (): any => ({
        id: this.id,
        overlayLayerIds: this.overlayLayerIds.join(AppSettings.Separator),
        backgroundLayerId: this.backgroundLayerId,
        offlineBboxJson: this.offlineBboxJson,
        trackingLayerFilterJson: this.layerFilterJson,
        lastMigrationId: this.lastMigrationId,
    })
}
