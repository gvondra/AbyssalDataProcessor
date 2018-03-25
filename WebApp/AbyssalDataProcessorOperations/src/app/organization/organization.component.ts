import 'rxjs/add/operator/switchMap';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Organization } from '../organization';
import { OrganizationService } from '../organization.service';
@Component({
  selector: 'app-organization',
  templateUrl: './organization.component.html',
  styleUrls: ['./organization.component.css'],
  providers: [OrganizationService]
})
export class OrganizationComponent implements OnInit {

  Organization: Organization = null;
  SpinnerHidden: boolean = true;

  constructor(private route: ActivatedRoute, private organizationService: OrganizationService) { }

  Submit() {
    this.SpinnerHidden = false;
    this.organizationService.putOrganization(this.Organization)
    .then(org => {
      this.Organization = org;
      this.SpinnerHidden = true;
    })
  }

  ngOnInit() {
    this.route.params
    .switchMap((params: ParamMap) => { 
      this.SpinnerHidden = false;
      if (params['id']) {        
        return this.organizationService.getOrganization(params['id']);
      }
      else {
        return Promise.resolve(new Organization());
      }      
    })
    .subscribe(org => {
      this.Organization = org;    
      this.SpinnerHidden = true;
    });
  }

}
