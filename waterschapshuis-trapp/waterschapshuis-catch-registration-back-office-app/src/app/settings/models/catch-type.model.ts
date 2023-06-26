import { AnimalType, GetCatchTypeResponse,  } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';
import { animals } from './animal-type.model';


export class CatchType {
    id?: string;
    name?: string;
    isByCatch?: boolean;
    animalType?: AnimalType;
    order?: number;

    public get animalTypeTitle(): string {
        return animals.find(x => x.value === this.animalType).title;
    }

    constructor(
        id: string,
        name: string,
        isByCatch: boolean,
        animalType: AnimalType,
        order?: number
    ) {
        this.id = id;
        this.name = name;
        this.isByCatch = isByCatch;
        this.animalType = animalType;
        this.order = order;
    }


    static fromResponse(response: GetCatchTypeResponse): CatchType {
        return new CatchType(
            response.id,
            response.name,
            response.isByCatch,
            response.animalType,
            response.order
        );
    }

}
