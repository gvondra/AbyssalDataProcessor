import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth/auth.guard';
import { CallbackComponent } from './callback/callback.component';
import { RoleRequestComponent } from './role-request/role-request.component';
import { UserSearchComponent } from './user-search/user-search.component';
import { UserComponent} from './user/user.component';
import { GroupListComponent } from './group-list/group-list.component';
import { GroupComponent } from './group/group.component';
import { TaskTypeListComponent } from './task-type-list/task-type-list.component';
import { TaskTypeComponent } from './task-type/task-type.component';
import { EventTypeListComponent } from './event-type-list/event-type-list.component';
import { EventTypeComponent } from './event-type/event-type.component';
import { UserGroupsComponent } from './user-groups/user-groups.component';
import { TaskTypeEventTypesComponent } from './task-type-event-types/task-type-event-types.component';
import { TaskTypeGroupsComponent } from './task-type-groups/task-type-groups.component';
import { WebMetricsComponent } from './web-metrics/web-metrics.component';
import { UnassignedTasksComponent } from './unassigned-tasks/unassigned-tasks.component';
import { MyTasksComponent } from './my-tasks/my-tasks.component';
import { TaskComponent } from './task/task.component';
import { OrganizationsComponent } from './organizations/organizations.component';
import { OrganizationComponent } from './organization/organization.component';
const routes: Routes = [
    {
        path: '',
        component: HomeComponent
      },
      {
        path: 'callback',
        component: CallbackComponent
      },
      {
        path: 'rolerequest',
        component: RoleRequestComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'usersearch',
        component: UserSearchComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'user/:id/groups',
        component: UserGroupsComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'user/:id',
        component: UserComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'grouplist',
        component: GroupListComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'group/:id',
        component: GroupComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'group',
        component: GroupComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'tasktypelist',
        component: TaskTypeListComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'task/:id',
        component: TaskComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'tasktype/:id/eventtypes',
        component: TaskTypeEventTypesComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'tasktype/:id/groups',
        component: TaskTypeGroupsComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'tasktype/:id',
        component: TaskTypeComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'tasktype',
        component: TaskTypeComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'eventtypelist',
        component: EventTypeListComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'eventtype/:id',
        component: EventTypeComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'webmetrics',
        component: WebMetricsComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'unassignedtasks',
        component: UnassignedTasksComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'mytasks',
        component: MyTasksComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'organizations',
        component: OrganizationsComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'organization',
        component: OrganizationComponent,
        canActivate: [
          AuthGuard
        ]
      },
      {
        path: 'organization/:id',
        component: OrganizationComponent,
        canActivate: [
          AuthGuard
        ]
      }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }