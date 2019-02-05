import { Injectable } from '@angular/core';
import { FrameSection, FrameIn360View, Frame } from '../../models/frame/frame.models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FramesService {
  private apiRootUri = '/api/frameSections';

  constructor(private httpClient: HttpClient) { }

  getListOfFrames(): Observable<FrameSection[]> {
      return this.httpClient.get<FrameSection[]>(this.apiRootUri);
  }

  getFrameById(frameSectionId: number): Observable<FrameSection> {
      const frameUrl = `${this.apiRootUri}/${frameSectionId}`;
      return this.httpClient.get<FrameSection>(frameUrl);
  }
}
