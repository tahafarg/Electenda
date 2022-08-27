import { Component, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignOutComponent } from './sign-out/sign-out.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
// import { SharedModule } from '../Shared/shared/shared.module';

let AuhPaths :Routes = [
  {path: '', redirectTo:'SignIn',pathMatch:"full"},
  {path : "SignIn", component : SignInComponent},
  {path : "SignUp", component : SignUpComponent},
  {path : "SignOut", component : SignOutComponent}
]

@NgModule({
  declarations: [
    SignInComponent,
    SignUpComponent,
    SignOutComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    RouterModule.forChild(AuhPaths)
  ],
  exports: [
    SignInComponent,
    SignUpComponent,
    SignOutComponent,
    RouterModule
  ]
})
export class AuthModule { }
