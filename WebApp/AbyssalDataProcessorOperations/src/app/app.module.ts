import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router'
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthService } from './auth/auth.service';
import { CallbackComponent } from './callback/callback.component';
import { RoleRequestComponent } from './role-request/role-request.component';
import { UserSearchComponent } from './user-search/user-search.component';
import { UserComponent } from './user/user.component';
import { DateValueAccessorModule } from './date-value-accessor/date-value-accessor.module';
import { GroupListComponent } from './group-list/group-list.component';
import { GroupComponent } from './group/group.component';
import { TaskTypeComponent } from './task-type/task-type.component';
import { TaskTypeListComponent } from './task-type-list/task-type-list.component';
import { EventTypeListComponent } from './event-type-list/event-type-list.component';
import { EventTypeComponent } from './event-type/event-type.component';
import { UserGroupsComponent } from './user-groups/user-groups.component';
import { TaskTypeEventTypesComponent } from './task-type-event-types/task-type-event-types.component';
import { TaskTypeGroupsComponent } from './task-type-groups/task-type-groups.component';
import { WebMetricsComponent } from './web-metrics/web-metrics.component';
import { UnassignedTasksComponent } from './unassigned-tasks/unassigned-tasks.component';
import { MyTasksComponent } from './my-tasks/my-tasks.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    CallbackComponent,
    RoleRequestComponent,
    UserSearchComponent,
    UserComponent,
    GroupListComponent,
    GroupComponent,
    TaskTypeComponent,
    TaskTypeListComponent,
    EventTypeListComponent,
    EventTypeComponent,
    UserGroupsComponent,
    TaskTypeEventTypesComponent,
    TaskTypeGroupsComponent,
    WebMetricsComponent,
    UnassignedTasksComponent,
    MyTasksComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
    DateValueAccessorModule
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
