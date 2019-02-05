export interface PeriodElement {
    position: number;
    name: string;
    weight: number;
    symbol: string;
    actions: any;
}

export let ELEMENTS: PeriodElement[] = [
    {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H', actions: null},
    {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He', actions: null},
    {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li', actions: null},
    {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be', actions: null},
    {position: 5, name: 'Boron', weight: 10.811, symbol: 'B', actions: null},
    {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C', actions: null},
    {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N', actions: null},
    {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O', actions: null},
    {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F', actions: null},
    {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne', actions: null},
];
