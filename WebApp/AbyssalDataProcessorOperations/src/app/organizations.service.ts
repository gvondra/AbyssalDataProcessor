import 'rxjs/add/operator/toPromise';
import { Headers, Http, URLSearchParams } from '@angular/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Organization } from './organization';
@Injectable()
export class OrganizationsService {

  constructor(private http: Http) { }

  Search(searchText: string): Promise<Array<Organization>> { 
    let query = new URLSearchParams();
    query.append("s", searchText);
    return this.http.get(environment.baseUrl + "Organizations/Search", {
      headers: new Headers({"Authorization": `Bearer ${localStorage.getItem('token')}`}),
      params: query
    })
    .toPromise()
    .then(response => response.json() as Array<Organization>);
  }
}
