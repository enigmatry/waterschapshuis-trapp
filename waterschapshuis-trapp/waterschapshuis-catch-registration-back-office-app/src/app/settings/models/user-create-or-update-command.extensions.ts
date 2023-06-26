import { IGetCurrentUserProfileResponse, IGetUsersResponseItem, UserUpdateCommand } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export function mapToCommand(item: IGetUsersResponseItem | IGetCurrentUserProfileResponse): UserUpdateCommand {
    return new UserUpdateCommand({
        id: item.id,
        authorized: item.authorized,
        organizationId: item.organizationId,
    });
}
