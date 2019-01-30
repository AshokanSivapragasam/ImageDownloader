import { Component, Output } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { Meeting } from '../../common/meeting';
import { MeetingProvider } from '../../providers/meeting/meeting';
import { Participant } from '../../common/participant';

@Component({
  selector: 'meeting-schedule',
  templateUrl: 'meeting-schedule.html'
})
export class MeetingSchedulePage {
  @Output() _meeting_: Meeting;
  teamMembers: Array<Participant> = [];

  constructor(public navCtrl: NavController,
              public navParams: NavParams,
              private meetingProvider: MeetingProvider) {
    var meetingMayStartAt = new Date();
    var meetingMayEndAt = new Date();
    meetingMayEndAt.setHours(meetingMayStartAt.getHours() + 1);

    this._meeting_ = {
      title: 'Meeting 00',
      description: 'This is a sample description',
      startsAt: meetingMayStartAt.toISOString(),
      endsAt: meetingMayEndAt.toISOString(),
      location: 'Skype',
      durationInMinutes: 60,
      remindBefore: 5,
      priority: 1
    } as Meeting;

    this.teamMembers = [{
        name: 'Ashokan Sivapragasam',
        gravatorUri: 'http://localhost/vault/images/thumbnails/144-2015-228x160.jpg',
        pickedIn: false
      }, {
        name: 'Vinoth Sivapragasam',
        gravatorUri: 'http://localhost/vault/images/thumbnails/Eetti-2015-228x160.jpg',
        pickedIn: false
      }, {
        name: 'Prasanna Sivapragasam',
        gravatorUri: 'http://localhost/vault/images/thumbnails/Baahubali-2015_228x160.jpg',
        pickedIn: false
      }, {
        name: 'Shweta Gupta',
        gravatorUri: 'http://localhost/vault/images/thumbnails/Bajirao-Mastani-2015-228x160.jpg',
        pickedIn: false
      }, {
        name: 'Ashish Singla',
        gravatorUri: 'http://localhost/vault/images/thumbnails/Jackie-Chan-Adventures-Season-1-13-Episodes_228x160.jpg',
        pickedIn: false
      }, {
        name: 'Khushboo',
        gravatorUri: 'http://localhost/vault/images/thumbnails/The-Martian-2015_228x160.jpg',
        pickedIn: false
      }, {
        name: 'Asha',
        gravatorUri: 'http://localhost/vault/images/thumbnails/Baahubali-2015_228x160.jpg',
        pickedIn: false
      }, {
        name: 'Kranthi',
        gravatorUri: 'http://localhost/vault/images/thumbnails/Bajirao-Mastani-2015-228x160.jpg',
        pickedIn: false
      }, {
        name: 'Jacob',
        gravatorUri: 'http://localhost/vault/images/thumbnails/Jackie-Chan-Adventures-Season-1-13-Episodes_228x160.jpg',
        pickedIn: false
      }, {
        name: 'Dapinder',
        gravatorUri: 'http://localhost/vault/images/thumbnails/The-Martian-2015_228x160.jpg',
        pickedIn: false
      }
    ];
  }

  addMeeting(meeting: Meeting): void {
    meeting.durationInMinutes = ((Date.parse(meeting.endsAt) - Date.parse(meeting.startsAt))/ 60000);
    meeting.participants = this.teamMembers.filter(r => r.pickedIn === true);
    this.meetingProvider.addMeeting(meeting)
    .subscribe(meeting => { this.navCtrl.pop(); });    
  }
}
