import { Component, OnInit, OnChanges, DoCheck, AfterContentInit, AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-tracker',
  templateUrl: './tracker.component.html',
  styleUrls: ['./tracker.component.css']
})
export class TrackerComponent implements OnChanges, OnInit, DoCheck, AfterContentInit, 
                              AfterContentChecked, AfterViewInit, AfterViewChecked, OnDestroy {

  constructor() { }

  ngOnChanges() { console.log('OnChange'); }
  ngOnInit() { console.log('OnInit'); }
  ngDoCheck() { console.log('DoCheck'); }
  ngAfterContentInit() { console.log('AfterContentInit'); }
  ngAfterContentChecked() { console.log('AfterContentChecked'); }
  ngAfterViewInit() { console.log('AfterViewInit'); }
  ngAfterViewChecked() { console.log('AfterViewChecked'); }
  ngOnDestroy() { console.log('OnDestroy'); }
}
