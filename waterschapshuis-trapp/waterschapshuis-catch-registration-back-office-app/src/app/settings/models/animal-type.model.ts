import { AnimalType } from 'src/app/api/waterschapshuis-catch-registration-backoffice-api';

const animals = [

    {
        value: 1,
        name: AnimalType.Mammal,
        title: 'Zoogdieren'
    },
    {
        value: 2,
        name: AnimalType.Bird,
        title: 'Vogels'
    },
    {
        value: 3,
        name: AnimalType.Fish,
        title: 'Vissen'
    },
    {
        value: 4,
        name: AnimalType.Other,
        title: 'Overige diersoorten'
    }

];

export { animals };
