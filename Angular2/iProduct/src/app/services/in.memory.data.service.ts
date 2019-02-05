import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';

@Injectable()
export class InMemoryDataService implements InMemoryDbService {
    createDb() {
        const students = [
            {
                id: 1,
                name: 'Ashok',
                age: 17,
                course: 'IT'
            }, {
                id: 2,
                name: 'Vinoth',
                age: 16,
                course: 'Mech'
            }, {
                id: 3,
                name: 'Prasanna',
                age: 15,
                course: 'CSE'
            }, {
                id: 1,
                name: 'Ashok',
                age: 17,
                course: 'IT'
            }, {
                id: 2,
                name: 'Vinoth',
                age: 16,
                course: 'Mech'
            }, {
                id: 3,
                name: 'Prasanna',
                age: 15,
                course: 'CSE'
            }, {
                id: 1,
                name: 'Ashok',
                age: 17,
                course: 'IT'
            }, {
                id: 2,
                name: 'Vinoth',
                age: 16,
                course: 'Mech'
            }, {
                id: 3,
                name: 'Prasanna',
                age: 15,
                course: 'CSE'
            }, {
                id: 1,
                name: 'Ashok',
                age: 17,
                course: 'IT'
            }, {
                id: 2,
                name: 'Vinoth',
                age: 16,
                course: 'Mech'
            }, {
                id: 3,
                name: 'Prasanna',
                age: 15,
                course: 'CSE'
            }
        ];

        return { students };
    }
}
