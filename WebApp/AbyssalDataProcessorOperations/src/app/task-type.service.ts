import 'rxjs/add/operator/toPromise';
import { Headers, Http, URLSearchParams } from '@angular/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { TaskType } from './task-type';
import { TaskTypeEventType } from './task-type-event-type';

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
  
  getTaskTypeEventTypes(taskTypeId: string) : Promise<Array<TaskTypeEventType>> {
    let query = new URLSearchParams();
    query.append("allEventTypes", "true");
    return this.http.get(environment.baseUrl + "TaskType/" + taskTypeId + "/EventTypes", {
      headers: new Headers({"Authorization": `Bearer ${localStorage.getItem('token')}`}),
      params: query
    })
    .toPromise()
    .then(response => response.json() as Array<TaskTypeEventType>)
  }
  
  putTaskTypeEventTypes(userId: string, eventTypes: Array<TaskTypeEventType>) : Promise<string> {
    return this.http.put(environment.baseUrl + "TaskType/" + userId + "/EventTypes", eventTypes,
    {
      headers: new Headers({"Authorization": `Bearer ${localStorage.getItem('token')}`})
    })
    .toPromise()
    .then(response => response.text() as string)
  }
}
