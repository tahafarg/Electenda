// import { HttpBackend } from '@angular/common/http';
// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { Router } from '@angular/router';
// import { loginViewModel } from 'src/app/models/Account';
// import { AccountServices } from 'src/app/Services/Account';

// @Component({
//   selector: 'app-login',
//   templateUrl: './login.component.html',
//   styleUrls: ['./login.component.css']
// })
// export class LoginComponent implements OnInit {

//   constructor(private builder:FormBuilder,private acc:AccountServices,private router:Router) { }
//   form:FormGroup = new FormGroup([]);
//   ngOnInit(): void {
//     this.form = this.builder.group({
//       UserName: ['',
//         [
//           Validators.required,
//           Validators.minLength(5),
//         ],
//       ],
//       Password: ['',
//         [Validators.required, Validators.minLength(8)],
//       ]
//     });
//   }
// add(){
//   let log = new loginViewModel();
//   log.UserName = this.form.value["UserName"]
//   log.Password = this.form.value["Password"]
//   this.acc.login(log).subscribe(res=>{
//     if(res.Success){
//       localStorage.setItem('token',res.data);
//       localStorage.setItem('username',log.UserName);
//       this.acc.setLooggedStatus(true);
//       this.router.navigateByUrl('/')
//     }
//     else
//     alert('Try again!!!!!!!')
//   },
//   err=>alert(err))
// }
// }
