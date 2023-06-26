import { UsersWithRegisteredTimePerRayon } from './users-with-registered-time-per-rayon';
import { SidebarUserType, TimeRegistrationUser } from './time-registrations-user';
import { IGetTimeRegistrationPerRayonResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class UsersWithRegisteredTime {
    usersPerRayon: UsersWithRegisteredTimePerRayon[];
    usersInOrganization: TimeRegistrationUser[];
    usersWithTimeRegistrationGeneralItems: TimeRegistrationUser[];

    constructor(
        usersWithRegisteredTimePerRayon: UsersWithRegisteredTimePerRayon[],
        allUsersInOrganization: TimeRegistrationUser[],
        usersWithTimeRegistrationGeneralItems: TimeRegistrationUser[]) {
        this.usersPerRayon = usersWithRegisteredTimePerRayon;
        this.usersInOrganization = allUsersInOrganization;
        this.usersWithTimeRegistrationGeneralItems = usersWithTimeRegistrationGeneralItems;
    }

    static fromResponse(response: IGetTimeRegistrationPerRayonResponse): UsersWithRegisteredTime {
        return new UsersWithRegisteredTime(
            UsersWithRegisteredTimePerRayon.fromResponse(
                response.usersWithRegisteredTimePerRayon,
                SidebarUserType.usersWithRegisteredTimePerRayon),
            TimeRegistrationUser.fromResponse(response.usersInOrganization, SidebarUserType.usersInOrganization),
            TimeRegistrationUser.fromResponse(
                response.usersWithTimeRegistrationGeneralItems,
                SidebarUserType.usersWithTimeRegistrationGeneralItems));
    }
}
