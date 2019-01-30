import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FramesService } from '../../../services/frames/frames.service';
import { Frame } from '../../../models/frame/frame.models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-display-frame',
  templateUrl: './display-frame.component.html',
  styleUrls: ['./display-frame.component.css']
})
export class DisplayFrameComponent implements OnInit {
  queryParams;
  routeParams;
  frameSectionId: number;
  frameId: number;
  frame: Frame;
  currentFrameIn360ViewIndex = 0;
  @ViewChild('realImage') realImage: ElementRef;
  @ViewChild('zoomLens') zoomLens: ElementRef;

  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private framesService: FramesService) { }

  ngOnInit() {
    this.queryParams = this.activatedRoute.snapshot.queryParams;
    this.routeParams = this.activatedRoute.snapshot.params;
    this.frameSectionId = this.routeParams.frameSectionId;
    this.frameId = this.routeParams.frameId;
    this.loadFrameById(this.frameSectionId, this.frameId);
  }

  alertMe(): void {
    console.log('mouseover');
  }

  loadFrameById(frameSectionId: number, frameId: number): void {
    this.framesService.getFrameById(frameSectionId)
    .subscribe(_frameSection_ => {
      this.frame = _frameSection_.frames.find(x => x.id.toString() === frameId.toString());
    });
  }

  zoomThisFrame(frameIn360ViewIndex: number): void {
    this.currentFrameIn360ViewIndex = frameIn360ViewIndex;
  }
}
