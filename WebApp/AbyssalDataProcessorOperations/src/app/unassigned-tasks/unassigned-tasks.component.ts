import { Component, OnInit } from '@angular/core';
import { UnassignedTasksService } from '../unassigned-tasks.service';
import { UnassignedTask } from '../unassigned-task';

@Component({
  selector: 'app-unassigned-tasks',
  templateUrl: './unassigned-tasks.component.html',
  styleUrls: ['./unassigned-tasks.component.css'],
  providers: [UnassignedTasksService]
})
export class UnassignedTasksComponent implements OnInit {

  TaskTypes: Array<string> = null;  
  SelectedTaskType: string = null;
  Groups: Array<string> = null;
  Tasks: Array<UnassignedTask> = null;
  TasksFiltered: Array<UnassignedTask> = null;
  SpinnerHidden: boolean = false;

  constructor(private unassignedTasksService: UnassignedTasksService) { }

  ngOnInit() {
    this.unassignedTasksService.getByUser()
    .then(tasks => {
      this.SetTasks(tasks);
      this.SpinnerHidden = true;
    })
  }

  TaskTypeChange(event) {
    this.TasksFiltered = null;
    this.Groups = ["-- Select --"];
    this.SelectedTaskType = event.target.value
    let i = 0;
    if (this.Tasks) {
      for (let t of this.Tasks) {
        if (t.TaskTypeTitle === this.SelectedTaskType && !this.Groups.includes(t.GroupName)) {
          this.Groups.push(t.GroupName);
          i += 1;
        }
      }
    }
    if (i === 0) {
      this.Groups = null;
    }
  }

  GroupChange(event) {
    this.TasksFiltered = [];
    if (this.Tasks) {
      for (let t of this.Tasks) {
        if (t.TaskTypeTitle === this.SelectedTaskType && t.GroupName === event.target.value) {
          this.TasksFiltered.push(t);
        }
      }
    }    
  }

  FormatTimestamp(ts): string {
    if (ts && Date.parse(ts)) {
      let d = new Date(Date.parse(ts));
      ts = d.toLocaleDateString();
    }
    return ts
  }

  SetTasks(tasks: Array<UnassignedTask>): void {
    this.TasksFiltered = null;    
    let tt = ["-- Select --"];
    for (let t of tasks) {
      if (!tt.includes(t.TaskTypeTitle)) {
        tt.push(t.TaskTypeTitle);
      }
    }
    this.Tasks = tasks;
    this.TaskTypes = tt;
  }
}
