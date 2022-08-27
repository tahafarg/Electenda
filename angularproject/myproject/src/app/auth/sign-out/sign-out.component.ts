import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-sign-out',
  templateUrl: './sign-out.component.html',
  styleUrls: ['./sign-out.component.css']
})
export class SignOutComponent implements OnInit {

  constructor(private Auth : AuthService, private router : Router) { }

  ngOnInit(): void {
    this.Auth.SignOut().subscribe(
     res=>{
      localStorage.removeItem('token');
      localStorage.removeItem('Email');
      localStorage.removeItem('userId');
      this.Auth.setLooggedStatus(false);
       this.router.navigateByUrl('Auth/SignIn');
      //  this.Auth.IsLoggedIn()
      }
    )
  }

}
