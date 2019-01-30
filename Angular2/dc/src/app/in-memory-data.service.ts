import { InMemoryDbService } from 'angular-in-memory-web-api';

export class InMemoryDataService implements InMemoryDbService {
  createDb() {
      const characters = [
        {
          id: 1,
          name: 'Superman',
          gravatorUri: 'https://pre00.deviantart.net/3540/th/pre/i/2013/143/0/1/man_of_steel_by_rehsup-d666mbz.png',
          addressLine01: 'Address line 001',
          addressLine02: 'Address line 002',
          powers: [
              'Super-flight',
              'Super-freeze',
              'Super-speed',
              'Super-power',
              'Super-heavy',
              'Super-vision'
          ],
          sidekicks: [
              'Shazam'
          ],
          affliates: [
              'Justice League'
          ],
          villians: [
              'Darkseid',
              'General Zod'
          ]
      },
      {
          id: 2,
          name: 'Batman',
          gravatorUri: 'https://www.pixelstalk.net/wp-content/uploads/2016/05/Batman-Logo-Wallpaper-by-Artieftw.png',
          addressLine01: 'Address line 001',
          addressLine02: 'Address line 002',
          powers: [
              'Martial-arts',
              'Bat-tactics',
              'Bat-stealth',
              'Bat-intellect',
              'Bat-mobile',
              'Bat-signal',
              'Rich'
          ],
          sidekicks: [
              'Robin',
              'Catwoman'
          ],
          affliates: [
              'Justice League'
          ],
          villians: [
              'Darkseid',
              'Joker'
          ]
      },
      {
          id: 3,
          name: 'Wonder Woman',
          gravatorUri: 'https://i.ytimg.com/vi/xipW9b59TYQ/maxresdefault.jpg',
          addressLine01: 'Address line 001',
          addressLine02: 'Address line 002',
          powers: [
              'Martial-arts',
              'Wonder-speed',
              'Wonder-durability',
              'Wonder-strength',
              'Wonder-shield',
              'Wonder-sword',
              'Wonder-tiara',
              'Wonder-lasso',
              'Wonder-bracelets'
          ],
          sidekicks: [
              'Steve Trevor'
          ],
          affliates: [
              'Justice League'
          ],
          villians: [
              'Darkseid',
              'EvilBro'
          ]
      },
      {
        id: 4,
        name: 'Firestorm',
        gravatorUri: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSmvM2_hoDuMvNarYQIzPrvG5H46RYak6e1a10B340z69TyuJbW',
        addressLine01: 'Address line 001',
        addressLine02: 'Address line 002',
        powers: [
            'Martial-arts',
            'Fire-speed',
            'Fire-durability',
            'Wonder-strength',
            'Fire-sheild',
            'Nucleokinesis',
            'Ancestral Memory',
            'Gestalt Form',
            'Transmutation',
            'Clairvoyance'
        ],
        sidekicks: [
            'Steve Trevor'
        ],
        affliates: [
            'Justice League'
        ],
        villians: [
            'Darkseid',
            'EvilBro'
        ]
      },
      {
        id: 5,
        name: 'Aquaman',
        gravatorUri: 'https://vignette.wikia.nocookie.net/dcmovies/images/b/bd/Logo_aquaman.jpg/revision/latest?cb=20160724030752',
        addressLine01: 'Address line 001',
        addressLine02: 'Address line 002',
        powers: [
            'Martial-arts',
            'Fire-speed',
            'Fire-durability',
            'Wonder-strength',
            'Fire-sheild',
            'Nucleokinesis',
            'Ancestral Memory',
            'Gestalt Form',
            'Transmutation',
            'Clairvoyance'
        ],
        sidekicks: [
            'Steve Trevor'
        ],
        affliates: [
            'Justice League'
        ],
        villians: [
            'Darkseid',
            'EvilBro'
        ]
      },
      {
        id: 6,
        name: 'Flash',
        gravatorUri: 'https://i.redd.it/kdg50ye3wddx.png',
        // 'https://i.redd.it/kdg50ye3wddx.png',
        addressLine01: 'Address line 001',
        addressLine02: 'Address line 002',
        powers: [
            'Martial-arts',
            'Fire-speed',
            'Fire-durability',
            'Wonder-strength',
            'Fire-sheild',
            'Nucleokinesis',
            'Ancestral Memory',
            'Gestalt Form',
            'Transmutation',
            'Clairvoyance'
        ],
        sidekicks: [
            'Steve Trevor'
        ],
        affliates: [
            'Justice League'
        ],
        villians: [
            'Darkseid',
            'EvilBro'
        ]
      },
      {
        id: 7,
        name: 'Cyborg',
        gravatorUri: 'https://images.alphacoders.com/763/thumb-350-763342.png',
        addressLine01: 'Address line 001',
        addressLine02: 'Address line 002',
        powers: [
            'Martial-arts',
            'Fire-speed',
            'Fire-durability',
            'Wonder-strength',
            'Fire-sheild',
            'Nucleokinesis',
            'Ancestral Memory',
            'Gestalt Form',
            'Transmutation',
            'Clairvoyance'
        ],
        sidekicks: [
            'Steve Trevor'
        ],
        affliates: [
            'Justice League'
        ],
        villians: [
            'Darkseid',
            'EvilBro'
        ]
      }
    ];

    return { characters };
  }
}
