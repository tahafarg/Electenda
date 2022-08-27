import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ResultViewModel } from "../models/ResultViewModel";
import { CartAddViewModel, CartEditViewModel  } from "../models/Cart";
import { Product } from "../models/Product";


@Injectable()
export class CartServices
{
  
    constructor(private http:HttpClient){}
    url:string=environment.apiURl
    
    addCart(Cart:CartAddViewModel){
        return this.http.post<ResultViewModel>(this.url+ "Cart/add",Cart)
    }
    getCarts(id:string){
        return this.http.get<ResultViewModel>( environment.apiURl+"Cart/Getfororder?userid="+id)
    }

    // getCartByUser(id:string){
    //     return this.http.get<ResultViewModel>( environment.apiURl+"Cart/get?id="+id)
    // }
    
    
    getByID(id:number){
        return this.http.get<ResultViewModel>( environment.apiURl+"Cart/GetByID?id="+id)
    }

    editCart(cart:CartAddViewModel){
       
        return this.http.put<ResultViewModel>(this.url+"Cart/Editapi",cart)
    }

    
    deleteCart(id:number){
        return this.http.delete<ResultViewModel>(this.url+ "Cart/delete?id="+id)
    }


    
}