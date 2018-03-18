import 'rxjs/add/operator/switchMap';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { TaskType } from '../task-type';
import { TaskTypeService } from '../task-type.service';

@Component({
  selector: 'app-task-type',
  templateUrl: './task-type.component.html',
  styleUrls: ['./task-type.component.css'],
  providers: [TaskTypeService]
})
export class TaskTypeComponent implements OnInit {

  TaskType: TaskType = null;
  SpinnerHidden: boolean = true;

  constructor(private route: ActivatedRoute, private taskTypeService: TaskTypeService) { }

  Submit() {
    this.SpinnerHidden = false;
    this.taskTypeService.putGroup(this.TaskType)
    .then(taskType => {
      this.TaskType = taskType;
      this.SpinnerHidden = true;
    })
  }

  ngOnInit() {
    this.route.params
    .switchMap((params: ParamMap) => { 
      this.SpinnerHidden = false;
      if (params['id']) {        
        return this.taskTypeService.getTaskType(params['id']);
      }
      else {
        return Promise.resolve(new TaskType());
      }      
    })
    .subscribe(taskType => {
      this.TaskType = taskType;    
      this.SpinnerHidden = true;
    });
  }

}
