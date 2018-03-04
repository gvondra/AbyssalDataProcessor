import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth/auth.guard';
import { CallbackComponent } from './callback/callback.component';
import { FirstContactComponent } from './first-contact/first-contact.component';
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
        path: 'firstcontact',
        component: FirstContactComponent,
        canActivate: [AuthGuard]
      }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }