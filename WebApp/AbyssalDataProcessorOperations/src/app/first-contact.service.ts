import 'rxjs/add/operator/toPromise';
import { Headers, Http, URLSearchParams } from '@angular/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
@Injectable()
export class FirstContactService {

  constructor(private http: Http) { }

  firstContact(code: string): Promise<any> {
    let query = new URLSearchParams();
    query.append("code", code);
    return this.http.post(environment.baseUrl + "FirstContact", null, {
      withCredentials: true
      //headers: new Headers({"Authorization": `Bearer ${localStorage.getItem('token')}`}),
      //params: query
    })
    .toPromise()
    .then(response => console.log(response))
    .catch(this.handleError)
  }

  private handleError(error: any): Promise<any> {
    console.error("An error occurred", error);
    return Promise.reject(error.message || error)
  }
}
