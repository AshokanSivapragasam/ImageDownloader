import { Participant } from "./participant";

export class Meeting {
    id: number;
    title: string;
    description: string;
    startsAt: string;
    endsAt: string;
    durationInMinutes: number;
    location: string;
    remindBefore: number;
    priority: number;
    participants: Participant[];
}