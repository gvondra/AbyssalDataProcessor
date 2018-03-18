import { Component, OnInit } from '@angular/core';
import { Group } from '../group';
import { GroupsService } from '../groups.service';

@Component({
  selector: 'app-group-list',
  templateUrl: './group-list.component.html',
  styleUrls: ['./group-list.component.css'],
  providers: [GroupsService]
})
export class GroupListComponent implements OnInit {

  Groups: Array<Group> = null;
  SpinnerHidden: boolean = true;

  constructor(private groupsService: GroupsService) { }

  ngOnInit() {
    this.SpinnerHidden = false;
    this.groupsService.getAllGroups()
    .then(groups => {
      this.Groups = groups;
      this.SpinnerHidden = true;
    })
  }

}
