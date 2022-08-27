import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders} from '@angular/common/http';
import { ResultViewModel } from '../models/ResultViewModel';
import { SignUpViewModel } from '../models/SignUpViewModel';
import { SignInViewModel } from '../models/SignInViewModel';
import { BehaviorSubject, Subject } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  Logged= new BehaviorSubject<boolean>(false);
    constructor(private http:HttpClient){
        this.Logged.next(this.IsLoggedIn());
    }
    getLooggedStatus(){
       return this.Logged.asObservable();
    }
    setLooggedStatus(status:boolean){
        return this.Logged.next(status);
     }
  public SignUp(SignUp : SignUpViewModel)
  {
    return this.http.post<ResultViewModel>(environment.apiURl+"User/SignUpapi", SignUp) 
  }

  public SignIn(SignIn : SignInViewModel)
  {
    return this.http.post<ResultViewModel>(environment.apiURl+"User/SignInapi", SignIn) 
  }
  
  public SignOut()
  {
    let token=localStorage.getItem('token');
    let headers = new HttpHeaders({
      'Content-Type' : 'application/json',
      'Authorization' : `Bearer ${token}`
    });
    let options = {headers : headers};
    return this.http.get<ResultViewModel>(environment.apiURl+"User/SignOutapi",options);
  }

  IsLoggedIn():boolean{
    let token =localStorage.getItem('token')
    console.log
    if(token != null){
        return true;
    }
    return false;
}
}
