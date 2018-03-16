import 'rxjs/add/operator/switchMap';
import { Component, OnInit } from '@angular/core';
import { User } from '../user';
import { UserService } from '../user.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  providers: [UserService]
})
export class UserComponent implements OnInit {

  User: User = null;
  SpinnerHidden: boolean = true;

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: ParamMap) => {
      this.SpinnerHidden = false;
      return this.userService.getUser(params['id']);
    })
    .subscribe(user => {
      this.User = user;
      this.SpinnerHidden = true;
    });
  }

}
