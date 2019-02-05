import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ThirukkuralModel } from '../../models/thirukkural/thirukkural.models';

@Injectable()
export class ThirukkuralService {
    constructor(private httpClient: HttpClient) { }

    loadThirukkurals(): Observable<ThirukkuralModel> {
        return this.httpClient.get<ThirukkuralModel>('../../assets/thirukkural.json');
    }
}
