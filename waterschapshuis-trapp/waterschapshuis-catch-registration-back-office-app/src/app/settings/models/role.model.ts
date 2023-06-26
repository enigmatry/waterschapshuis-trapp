import { GetUsersResponseItemRole, GetRolesResponseItem } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
export class Role {
    id?: string;
    name?: string | undefined;
    checked: boolean;
    disabled: boolean;

    constructor(id: string, name: string, checked: boolean, canBeChanged: boolean) {
        this.id = id;
        this.name = name;
        this.checked = checked;
        this.disabled = !canBeChanged;
    }

    static fromResponse(allRoles: GetRolesResponseItem[], userRoles: GetUsersResponseItemRole[]): Role[] {
        const selectedRoles: Role[] = [];
        for (const role of allRoles) {
            const r = new Role(role.id, role.name, false, role.canCurrentUserChangeToThisRole);
            for (const assignedRole of userRoles) {
                if (assignedRole.id === role.id) {
                    r.checked = true;
                }
            }
            selectedRoles.push(r);
        }
        return selectedRoles;
    }
}
