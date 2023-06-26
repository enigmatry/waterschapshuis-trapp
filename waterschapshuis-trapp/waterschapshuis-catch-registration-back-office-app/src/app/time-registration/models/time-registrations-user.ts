import { IGetTimeRegistrationPerRayonResponseTimeRegistrationsPerRayonUser } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class TimeRegistrationUser {
    name: string;
    id: string;
    rayonId: string;
    weekCompleted: boolean;
    weekActive: boolean;
    rayons: string[];
    sideBarUserType: SidebarUserType;

    constructor(
        name: string,
        id: string,
        weekCompleted: boolean,
        weekActive: boolean,
        sideBarUserType: SidebarUserType
    ) {
        this.name = name;
        this.id = id;
        this.weekCompleted = weekCompleted;
        this.weekActive = weekActive;
        this.sideBarUserType = sideBarUserType;
    }

    static fromResponse(
        response: IGetTimeRegistrationPerRayonResponseTimeRegistrationsPerRayonUser[],
        userType: SidebarUserType): TimeRegistrationUser[] {
        return response.map(res => new TimeRegistrationUser(res.name, res.id, res.weekCompleted, res.weekActive, userType));
    }
}

export enum SidebarUserType {
    usersWithRegisteredTimePerRayon = 0,
    usersWithTimeRegistrationGeneralItems = 1,
    usersInOrganization = 2
}
