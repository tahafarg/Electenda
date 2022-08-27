import { Component, OnInit } from '@angular/core';
import { Category } from '../models/Category';
import { AuthService } from '../Services/auth.service';
import { CategoryServices } from '../Services/CategoryServices';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private Auth: AuthService , private catSer:CategoryServices ) {}
  ISLOG :Boolean = false;
  Email : string = "";
  userName: string = "";
  cats:Category[] = []

  ngOnInit(): void {
    console.log(this.ISLOG)
    this.Auth.getLooggedStatus().subscribe(
      res=>{
        console.log(res)
        this.ISLOG = res
      }
    )
    this.catSer.getCat().subscribe(res =>
      {
        this.cats = res.data
      })
      this.Email = localStorage.getItem("Email")!;
      if(this.Email != null)
      {

        this.userName = this.Email.split("@")[0];
      }

  }

  
  
      
}
