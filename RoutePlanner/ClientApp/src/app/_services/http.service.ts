import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  public host: string;

  constructor(private http: HttpClient) {
    this.host = environment.host;
   }

  public get(route: string) {
    return this.http.get(this.host + route);
  }
  public post(route: string, body) {
    return this.http.post(this.host + route, body);
  }
}
