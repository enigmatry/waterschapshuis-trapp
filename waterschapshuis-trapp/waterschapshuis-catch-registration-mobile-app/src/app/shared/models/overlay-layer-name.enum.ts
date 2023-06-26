export enum OverlayLayerName {
  Organizations = 'catch-registration-v3:Organizations',
  Rayons = 'catch-registration-v3:Rayons',
  CatchAreas = 'catch-registration-v3:CatchAreas',
  WaterAuthorities = 'catch-registration-v3:WaterAuthorities',
  SubAreas = 'catch-registration-v3:SubAreas',
  HourSquares = 'catch-registration-v3:HourSquares',
  Provinces = 'catch-registration-v3:Provinces',
  Observations = 'catch-registration-v3:Observations',
  TrackingLines = 'catch-registration-v3:TrackingLines',
  TrackingsByUser = 'catch-registration-v3:TrackingsByUser',
  TrackingsByTrappers = 'catch-registration-v3:TrackingsByTrappers',
  TrapDetails = 'catch-registration-v3:TrapDetails',
  SubAreaHourSquares = 'catch-registration-v3:SubAreaHourSquares',
  SubAreaHourSquareCatches = 'catch-registration-v3:SubAreaHourSquareCatches'
}

export function getContentLayers(): OverlayLayerName[] {
  return [OverlayLayerName.Observations,
  OverlayLayerName.TrackingLines,
  OverlayLayerName.TrackingsByUser,
  OverlayLayerName.TrackingsByTrappers,
  OverlayLayerName.TrapDetails];
}
