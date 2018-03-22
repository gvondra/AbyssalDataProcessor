import 'rxjs/add/operator/switchMap';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Task } from '../task';
import { TaskService } from '../task.service';
@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css'],
  providers: [ TaskService ]
})
export class TaskComponent implements OnInit {

  Task: Task = null;
  SpinnerHidden: boolean = true;

  constructor(private route: ActivatedRoute, private taskService: TaskService) { }

  FormatTimestamp(ts): string {
    if (ts && Date.parse(ts)) {
      let d = new Date(Date.parse(ts));
      ts = d.toLocaleString();
    }
    return ts
  }

  ngOnInit() {
    this.route.params
    .switchMap((params: ParamMap) => {
      this.SpinnerHidden = false;
      return this.taskService.getTask(params['id']);
    })
    .subscribe(task => {
      this.Task = task;
      this.SpinnerHidden = true;
    });
  }

}
