import { Component, OnInit } from '@angular/core';
import { Router, TitleStrategy } from '@angular/router';
import { AccountServices } from 'src/app/Services/Account';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private acc:AccountServices,private router:Router) { }

  ngOnInit(): void {
    this.acc.logout().subscribe(
      res=> {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        //
        this.router.navigateByUrl('login')
      },
      err=> console.log(err)
    )
    localStorage.removeItem('token');
    localStorage.removeItem('username');
    this.acc.setLooggedStatus(false);
    this.router.navigateByUrl('login')
  }

}
