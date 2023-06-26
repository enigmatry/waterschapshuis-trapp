export class Observation {
  id: string;
  type: number;
  image?: string;
  remarks: string;
  longitude?: number;
  latitude?: number;
  recordedOn: Date;
  syncedToApi?: number;
}
