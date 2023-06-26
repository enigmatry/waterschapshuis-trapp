import { CatchCreateOrUpdateCommand } from '../../api/waterschapshuis-catch-registration-mobile-api';

export class CatchCreateOrUpdateCommandVm extends CatchCreateOrUpdateCommand {

  isByCatch: boolean;
  type: string;


  static fromJS(data: any): CatchCreateOrUpdateCommandVm {
    data = typeof data === 'object' ? data : {};
    const result = new CatchCreateOrUpdateCommandVm();
    result.init(data);
    return result;
  }

  init(data?: any) {
    super.init(data);
    if (data) {
      this.isByCatch = data.isByCatch;
      this.type = data.type;
    }
  }

  toJSON(data?: any) {
    data = super.toJSON(data);
    data.isByCatch = this.isByCatch;
    data.type = this.type;
    return data;
  }
}
