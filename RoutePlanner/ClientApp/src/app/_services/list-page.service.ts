import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { environment } from 'src/environments/environment';
import { User } from '../..';

@Injectable({
  providedIn: 'root'
})
export class ListPageService {

  constructor(private http: HttpService) { }

  public getListAny() {
    return this.http.get(environment.anyList);
  }
  public postUser(){

    const body = {name: user.name}
  }
}
