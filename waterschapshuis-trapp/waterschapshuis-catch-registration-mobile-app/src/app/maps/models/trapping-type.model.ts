import { INamedEntityItem } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';

export class TrappingType {
  id?: string;
  name?: string;

  constructor(response: INamedEntityItem) {
    this.id = response.id;
    this.name = response.name;
  }

  static fromResponse(response: INamedEntityItem): TrappingType {
    return new TrappingType(response);
  }
}
