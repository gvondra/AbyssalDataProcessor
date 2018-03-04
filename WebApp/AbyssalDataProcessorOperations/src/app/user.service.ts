import 'rxjs/add/operator/toPromise';
import { Headers, Http, URLSearchParams } from '@angular/http';
import { Injectable } from '@angular/core';
import { User } from './user';
import { environment } from '../environments/environment';
@Injectable()
export class UserService {

  constructor(private http: Http) { }

  getUser() : Promise<User> {
    return this.http.get(environment.baseUrl + "User", {
      headers: new Headers({"Authorization": `Bearer ${localStorage.getItem('token')}`})
    })
    .toPromise()
    .then(response => response.json as User)
  }
}
