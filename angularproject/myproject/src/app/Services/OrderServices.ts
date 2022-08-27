import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ResultViewModel } from "../models/ResultViewModel";
// import {Order } from "../models/Order";
import {  CartAddViewModel } from "../models/Cart";


@Injectable()
export class OrderServices
{
    constructor(private http:HttpClient){}
    url:string = environment.apiURl;
    
    // addOrder(userid:string)
    // {
    //     return this.http.post<ResultViewModel>(this.url + 'Order/Add',Order)
    // }

    
addOrder(userid:string)
{
    return this.http.post<ResultViewModel>(this.url+"Order/Add?UserId="+userid,null)
}
       

}