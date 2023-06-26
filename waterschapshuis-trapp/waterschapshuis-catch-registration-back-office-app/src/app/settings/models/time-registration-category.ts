import { GetTimeRegistrationCategoryResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

export class TimeRegistrationCategory {
    id?: string;
    name?: string;
    active?: boolean;

    constructor(
        id?: string,
        name?: string,
        active?: boolean,
    ) {
        this.id = id;
        this.name = name;
        this.active = active;
    }

    static fromResponse(response: GetTimeRegistrationCategoryResponse): TimeRegistrationCategory {
        return new TimeRegistrationCategory(
            response.id,
            response.name,
            response.active
        );
    }
}
