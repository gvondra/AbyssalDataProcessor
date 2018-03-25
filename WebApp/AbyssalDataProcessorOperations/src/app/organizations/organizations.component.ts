import { Component, OnInit } from '@angular/core';
import { Organization } from '../organization';
import { OrganizationsService } from '../organizations.service';
@Component({
  selector: 'app-organizations',
  templateUrl: './organizations.component.html',
  styleUrls: ['./organizations.component.css'],
  providers: [OrganizationsService]
})
export class OrganizationsComponent implements OnInit {

  SearchText: string = "";
  SearchResult: Array<Organization> = null;
  SpinnerHidden: boolean = true;

  constructor(private organizationsService: OrganizationsService) { }

  Submit() {
    this.SpinnerHidden = false;
    this.SearchResult = null;
    this.organizationsService.Search(this.SearchText)
    .then(result => {
      this.SearchResult = result;
      this.SpinnerHidden = true;
    })
    .catch(ex => {
      this.SpinnerHidden = true;
      console.log(ex)
    })
  }

  ngOnInit() {
  }

}
