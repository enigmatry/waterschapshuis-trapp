export enum MapItemType {
  Trap,
  Observation
}

export class MapItemSelectionModel {
  id: string;
  type: MapItemType;

  static create(
    id: string,
    type: MapItemType
  ): MapItemSelectionModel {
    return {
      id,
      type
    };
  }
}
