import { IGetMySummaryTrapInfo } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';

export class UserTrapInfo {
    typeLabel?: string;
    dateCreated?: Date;
    daysSinceCatch?: number;

    static fromResponse(response: IGetMySummaryTrapInfo): UserTrapInfo {
        const userTrapInfo = new UserTrapInfo();
        userTrapInfo.typeLabel = response.typeLabel;
        userTrapInfo.dateCreated = response.dateCreated;
        userTrapInfo.daysSinceCatch = response.daysSinceCatch;
        return userTrapInfo;
    }
}
