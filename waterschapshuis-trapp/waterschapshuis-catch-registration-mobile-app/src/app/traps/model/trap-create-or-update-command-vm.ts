import { TrapCreateOrUpdateCommand, TrapStatus } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';

export class TrapCreateOrUpdateCommandVm extends TrapCreateOrUpdateCommand {
  previousStatus?: TrapStatus;
  numberOfCatches?: number;
  numberOfByCatches?: number;
  shouldDelete?: boolean;
  createdOn?: Date;
  createdBy?: string;

  static fromJS(data: any): TrapCreateOrUpdateCommandVm {
    data = typeof data === 'object' ? data : {};
    const result = new TrapCreateOrUpdateCommandVm();
    result.init(data);
    return result;
  }

  init(data?: any) {
    super.init(data);
    if (data) {
      this.previousStatus = data.previousStatus;
    }
  }

  toJSON(data?: any) {
    data = super.toJSON(data);
    data.previousStatus = this.previousStatus;
    return data;
  }
}
