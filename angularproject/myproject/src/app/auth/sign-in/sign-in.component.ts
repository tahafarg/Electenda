import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators,ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { SignInViewModel } from 'src/app/models/SignInViewModel'
import { AuthService } from '../../Services/auth.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  form:FormGroup = new FormGroup([]);
  constructor(private Auth : AuthService, private router : Router, private builder: FormBuilder ) { }

  ngOnInit(): void {
    this.form= this.builder.group({
      Email:['',[Validators.required, Validators.email]],
      Password:['',[Validators.required, Validators.minLength(6)]],
      RememberMe:[ false],
      })
  }

  logging()
  {
    let loggingUser = new SignInViewModel();
    loggingUser.Email = this.form.value["Email"];
    loggingUser.Password = this.form.value["Password"];
    loggingUser.RememberMe = this.form.value["RememberMe"];

    console.log(loggingUser);
    this.Auth.SignIn(loggingUser).subscribe(
      res => {
        console.log(res);
      
        if(res.success)
        {
          console.log(res.data.token)
          localStorage.setItem('Email', loggingUser.Email)
          localStorage.setItem('userId', res.data.userId)
          localStorage.setItem("token",res.data.token);
          
          this.Auth.setLooggedStatus(true);
        //  window.location.reload()
          this.router.navigateByUrl('')
          
        }
        else
        {
          console.log(res.message)
        }
      },
      err=>alert(err))
    
  }

  

}
