import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { MeetingProvider } from '../../providers/meeting/meeting';
import { Meeting } from '../../common/meeting';

@Component({
  selector: 'meeting-details',
  templateUrl: 'meeting-details.html'
})
export class MeetingDetailsPage {
  selectedMeeting: Meeting;
  selectedMeetingId: number;

  constructor(public navCtrl: NavController,
              public navParams: NavParams,
              public meetingProvider: MeetingProvider) {
    this.selectedMeetingId = navParams.get('meetingId');
    this.getMeeting(this.selectedMeetingId);
  }

  getMeeting(id: number): void {
    this.meetingProvider.getMeeting(id)
    .subscribe(meeting => this.selectedMeeting = meeting);
  }
}
