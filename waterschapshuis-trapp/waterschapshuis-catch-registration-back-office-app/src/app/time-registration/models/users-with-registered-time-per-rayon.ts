import { SidebarUserType, TimeRegistrationUser } from './time-registrations-user';
import { IGetTimeRegistrationPerRayonResponseTimeRegistrationsPerRayon } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class UsersWithRegisteredTimePerRayon {
    rayonId: string;
    rayonName: string;
    users: TimeRegistrationUser[] = [];

    constructor(rayonId: string, rayonName: string, users: TimeRegistrationUser[]) {
        this.rayonId = rayonId;
        this.rayonName = rayonName;
        this.users = users;
    }

    static fromResponse(
        response: IGetTimeRegistrationPerRayonResponseTimeRegistrationsPerRayon[],
        userType: SidebarUserType): UsersWithRegisteredTimePerRayon[] {
        return response
            .map(res => new UsersWithRegisteredTimePerRayon(
                res.rayonId,
                res.rayonName,
                TimeRegistrationUser.fromResponse(res.users, userType)));
    }
}
