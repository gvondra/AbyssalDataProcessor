import { Component, OnInit } from '@angular/core';
import { RoleRequest } from '../role-request';
@Component({
  selector: 'app-role-request',
  templateUrl: './role-request.component.html',
  styleUrls: ['./role-request.component.css']
})
export class RoleRequestComponent implements OnInit {

  roleRequest: RoleRequest;

  constructor() { }

  Submit() {
    
  }

  ngOnInit() {
    this.roleRequest = new RoleRequest();
    var p = JSON.parse(localStorage.getItem('profile'));    
    if (p) {
      this.roleRequest.FullName = p.name;
    }
  }

}
