import { GetTrapTypeResponse } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';


export class TrapType {
    id?: string;
    name?: string;
    trappingTypeId?: string;
    trappingType?: string;
    active?: boolean;
    order?: number;

    constructor(
        id?: string,
        name?: string,
        trappingTypeId?: string,
        trappingType?: string,
        active?: boolean,
        order?: number
    ) {
        this.id = id;
        this.name = name;
        this.trappingTypeId = trappingTypeId;
        this.trappingType = trappingType;
        this.active = active;
        this.order = order;
    }

    static fromResponse(response: GetTrapTypeResponse): TrapType {
        return new TrapType(
            response.id,
            response.name,
            response.trappingTypeId,
            response.trappingType,
            response.active,
            response.order
        );
    }

}
