import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AuthComponent} from "./auth/auth.component";
import {PasswordManagerComponent} from "./password-manager/password-manager.component";

const routes: Routes = [
  {path: 'login', component: AuthComponent},
  {path: '', component: AuthComponent}, 
  {path: 'main', component: PasswordManagerComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
