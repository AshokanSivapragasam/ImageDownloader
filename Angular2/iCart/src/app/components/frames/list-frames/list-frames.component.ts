import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FrameSection } from '../../../models/frame/frame.models';
import { FramesService } from '../../../services/frames/frames.service';

@Component({
  selector: 'app-list-frames',
  templateUrl: './list-frames.component.html',
  styleUrls: ['./list-frames.component.css']
})
export class ListFramesComponent implements OnInit {
  frameSections: FrameSection[];

  constructor(private framesService: FramesService) { }

  ngOnInit() {
    this.loadFrames();
  }

  loadFrames(): void {
    this.framesService.getListOfFrames()
    .subscribe(_frameSections_ => this.frameSections = _frameSections_);
  }
}
