import { JsonPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { from } from 'rxjs';
import { SignUpViewModel } from 'src/app/models/SignUpViewModel';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
 form:FormGroup=new FormGroup([]);
 //user: SignUpViewModel[] = []; 
  constructor(private Auth : AuthService, private router : Router, private builder: FormBuilder ) { }

  ngOnInit(): void {
    this.form= this.builder.group({
    FirstName:['',[Validators.required,Validators.minLength(3)]],
    LastName:['',[Validators.required,Validators.minLength(3)]],
    Address:['',[Validators.required,Validators.minLength(3)]],
    Email:['',[Validators.required, Validators.email]],
    Password:['',[Validators.required, Validators.minLength(6)]],
    ConfirmPassword:['',[Validators.required, Validators.minLength(6)]],
    })
  }

  add(){
   let user = new SignUpViewModel();
   user.FirstName = this.form.value["FirstName"];
   user.LastName=this.form.value["LastName"];
   user.Address=this.form.value["Address"];
   user.Email=this.form.value["Email"];
   user.Password=this.form.value["Password"]; 
   user.ConfirmPassword = this.form.value["ConfirmPassword"]
   //console.log(user);
   this.Auth.SignUp(user).subscribe(
     res=> {
      if(res.success == true)
      {
        this.router.navigateByUrl('/Auth/SignIn');
      }
      else{
            alert(res.data);
      }
    },

      err=>{
        console.log(err);
      }) 
   }


}
