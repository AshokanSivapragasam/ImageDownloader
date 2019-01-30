import { InMemoryDbService } from 'angular-in-memory-web-api';
import { Meeting } from './meeting';

export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    const meetings: Meeting[] = [];
    for (let idx = 1; idx < 3; idx +=1) {
      const meetingStartsAt = new Date();
      const meetingEndsAt = new Date();
      meetingEndsAt.setHours(meetingStartsAt.getHours() + Math.floor(Math.random() * 2));
      meetingEndsAt.setMinutes(meetingStartsAt.getMinutes() + Math.floor(Math.random() * 59));
      const meetingDurationInMinutes = (Date.parse(meetingEndsAt.toISOString()) - Date.parse(meetingStartsAt.toISOString()))/ 60000;
      meetings.push({
        id: idx,
        title: 'Daily standup call::' + idx,
        description: 'Design discussion on Modern Events. Index: ' + idx,
        startsAt: meetingStartsAt.toISOString(),
        endsAt: meetingEndsAt.toISOString(),
        durationInMinutes: meetingDurationInMinutes,
        location: 'Skype',
        remindBefore: 5,
        priority: 1,
        participants: [{
          name: 'Ashokan Sivapragasam',
          gravatorUri: 'http://localhost/vault/images/thumbnails/144-2015-228x160.jpg',
          pickedIn: true
        }, {
          name: 'Vinoth Sivapragasam',
          gravatorUri: 'http://localhost/vault/images/thumbnails/Eetti-2015-228x160.jpg',
          pickedIn: true
        }, {
          name: 'Prasanna Sivapragasam',
          gravatorUri: 'http://localhost/vault/images/thumbnails/Baahubali-2015_228x160.jpg',
          pickedIn: true
        }]
      });
    }

    return { meetings };
  }
}
