import { IGetUsersResponseItem, GetUsersResponseItemRole } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class UserRoles {
    id: string;
    email: string;
    name: string;
    rolesDisplayText: string;
    roles: GetUsersResponseItemRole[];

    constructor(id: string, email: string, name: string, rolesDisplayText: string, roles: GetUsersResponseItemRole[]) {
        this.id = id;
        this.email = email;
        this.name = name;
        this.rolesDisplayText = rolesDisplayText;
        this.roles = roles;
    }
    static fromResponse(response: IGetUsersResponseItem): UserRoles {
        const rolesString = Array.prototype.map.call(response.roles, (s: { name: any; }) => s.name).toString();
        return new UserRoles(response.id, response.email, response.name, rolesString, response.roles);
    }
}


