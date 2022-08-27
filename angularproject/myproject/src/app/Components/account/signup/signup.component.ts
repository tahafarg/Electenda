import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SignUpViewModel } from 'src/app/models/Account';
import { AccountServices } from 'src/app/Services/Account';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  constructor(private builder:FormBuilder,private acc:AccountServices,private router:Router) { }
  form:FormGroup = new FormGroup([]);
  loading=false;
  ngOnInit(): void {
    this.form = this.builder.group({
      Name: ['',
        [
          Validators.required,
          Validators.minLength(5),
        ],
      ],
      UserName: ['',
        [
          Validators.required,
          Validators.minLength(5),
        ],
      ],
      Password: ['',
        [Validators.required, Validators.minLength(8)],
      ]
    });
  }
add(){
this.loading = true;
  let log = new SignUpViewModel();
  log.UserName = this.form.value["UserName"]
  log.Name = this.form.value["Name"]
  log.Password = this.form.value["Password"]
  this.acc.signup(log).subscribe(res=>{
    if(res.Success){
      this.router.navigateByUrl('/login')
    }
    else{
      this.loading=false;
      alert('Try again!!!!!!!')
    }
  },
  err=>{
    alert(err);
    this.loading=false;
  })
}

}
