import 'rxjs/add/operator/toPromise';
import { Headers, Http, URLSearchParams } from '@angular/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Organization } from './organization';

@Injectable()
export class OrganizationService {

  constructor(private http: Http) { }

  getOrganization(organizationId: string) : Promise<Organization> {
    return this.http.get(environment.baseUrl + "Organization/" + organizationId, {
      headers: new Headers({"Authorization": `Bearer ${localStorage.getItem('token')}`})
    })
    .toPromise()
    .then(response => response.json() as Organization)
  }

  putOrganization(organization: Organization): Promise<Organization> {
    return this.http.put(environment.baseUrl + "Organization", organization,
    {
      headers: new Headers({"Authorization": `Bearer ${localStorage.getItem('token')}`})
    })
    .toPromise()
    .then(response => response.json() as Organization)
  }
}
