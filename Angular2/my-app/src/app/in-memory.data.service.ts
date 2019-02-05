import { InMemoryDbService } from 'angular-in-memory-web-api';

export class InMemoryDataService implements InMemoryDbService {
    createDb() {
        const heroes = [
            { id: 1, name: 'Spidey' },
            { id: 2, name: 'Superman' },
            { id: 3, name: 'Ironman' },
            { id: 4, name: 'Batman' },
            { id: 5, name: 'Boomerman' },
            { id: 6, name: 'Silver Surfer' },
            { id: 7, name: 'Captain America' },
            { id: 8, name: 'Wolverine' },
            { id: 9, name: 'Jackie Chan' }
        ];

        return {heroes};
    }
}
