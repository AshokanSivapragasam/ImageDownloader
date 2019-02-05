export class Character {
    constructor(
        public id: number,
        public name: string,
        public gravatorUri: string,
        public addressLine01: string,
        public addressLine02: string,
        public powers: string[],
        public sidekicks: string[],
        public villians: string[],
        public affliates: string[]
    ) {}
}
