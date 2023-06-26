import { UserTrapInfo } from './user-trap-info';
import { IGetMySummaryResponse } from 'src/app/api/waterschapshuis-catch-registration-mobile-api';

export class UserSummary {
    catchesThisWeek?: number;
    outstandingTraps?: number;
    trapDetails?: UserTrapInfo[];

    static fromResponse(response: IGetMySummaryResponse): UserSummary {
        const userSummary = new UserSummary();
        userSummary.catchesThisWeek = response.catchesThisWeek;
        userSummary.outstandingTraps = response.outstandingTraps;
        userSummary.trapDetails =
            response.trapDetails
                .map(item =>
                    UserTrapInfo.fromResponse(item)
                );

        return userSummary;
    }
}
