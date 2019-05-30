import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { environment } from 'src/environments/environment';
import { Users } from '../../models/users.model';

@Injectable({
  providedIn: 'root'
})
export class ListPageService {

  constructor(private http: HttpService) { }

  public getListAny() {
    return this.http.get(environment.anyList);
  }
  public postUser(user: Users){

    const body = {login: user.login, pass: user.pass, email: user.email };
    return this.http.post(environment.anyList, body);
  }
}
