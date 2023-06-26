import { environment } from 'src/environments/environment';
import { BackgroundLayerName } from '../shared/models/background-layer-name.enum';
import { OverlayLayerName } from '../shared/models/overlay-layer-name.enum';

export class AppSettings {
  public static mapSettings = {
    hitTolerance: 5, // in pixels
    maxLookupResolution: 50, // for feature layers. 0 is de lowest layer (you will only see couple of meters),
                             // the higher the number the more data will be loaded. 17 is about Rayon level
    bboxExtendFactor: 1.2 //
  };

  public static userIdleSettings = {
    idle: Number(environment.userIdlePeriodInSec),
    timeout: 10,
    ping: 100
  };

  public static defaultSelectedLayers = {
    overlayLayers: [
      OverlayLayerName.HourSquares,
      OverlayLayerName.SubAreas,
      OverlayLayerName.CatchAreas,
      OverlayLayerName.Rayons,
      `${OverlayLayerName.TrapDetails}:TrapCreatedYear${new Date().getFullYear()}Active`
    ],
    background: BackgroundLayerName.OpenTopo
  };
}
