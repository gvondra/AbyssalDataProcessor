import 'rxjs/add/operator/toPromise';
import { Headers, Http, URLSearchParams } from '@angular/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { TaskType } from './task-type';

@Injectable()
export class TaskTypeService {

  constructor(private http: Http) { }

  getTaskType(taskTypeId: string) : Promise<TaskType> {
    return this.http.get(environment.baseUrl + "TaskType/" + taskTypeId, {
      headers: new Headers({"Authorization": `Bearer ${localStorage.getItem('token')}`})
    })
    .toPromise()
    .then(response => response.json() as TaskType)
  }

  putGroup(taskType: TaskType): Promise<TaskType> {
    return this.http.put(environment.baseUrl + "TaskType", taskType,
    {
      headers: new Headers({"Authorization": `Bearer ${localStorage.getItem('token')}`})
    })
    .toPromise()
    .then(response => response.json() as TaskType)
  }
}
