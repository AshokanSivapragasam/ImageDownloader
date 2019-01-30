export class Hero02 {
    id = 0;
    name = '';
    addresses: Address[];
    powers: string[];
    sidekicks: string[];
    villians: string[];
}

export class Address {
    street: string;
    city: string;
    state: string;
    zipcode: string;
}

export const heroes02: Hero02[] = [
    {
        id: 1,
        name: 'Superman',
        addresses: [
            {
                street: 'Right street',
                city: 'Downtown',
                state: 'Upstate',
                zipcode: 'cz1209'
            }
        ],
        powers: [
            'super-speed',
            'super-heat',
            'super-freeze',
            'super-flight',
            'super-intelligence'
        ],
        sidekicks: [],
        villians: [
            'General zod',
            'Nuclear man'
        ]
    },
    {
        id: 2,
        name: 'Spiderman',
        addresses: [
            {
                street: 'Right street',
                city: 'Downtown',
                state: 'Upstate',
                zipcode: 'cz1210'
            }
        ],
        powers: [
            'spidey-web',
            'spidey-strong',
            'spidey-flexible',
            'spidey-intelligence'
        ],
        sidekicks: [],
        villians: [
            'Sand man',
            'Electro man'
        ]
    }
];


export const states = ['Up state', 'Down state'];
