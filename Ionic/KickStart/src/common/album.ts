import { Disc } from "./disc";

export class Album {
    id: number;
    title: string;
    albumPictureUri: string;
    discs: Disc[];
}