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
        path: 'group',
        component: GroupComponent,
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
      }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }