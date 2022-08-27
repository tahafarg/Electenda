import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ResultViewModel } from "../models/ResultViewModel";
import { FavoriteAddViewModel } from "../models/Favourite";
//import { HttpClient, HttpHeaders } from "@angular/common/http";
//import { Injectable } from "@angular/core";
//import { environment } from "src/environments/environment";
//import { ResultViewModel } from "../models/ResultViewModel";
//import { CartAddViewModel, CartEditViewModel  } from "../models/Cart";
import { Product } from "../models/Product";



@Injectable({
  providedIn: 'root'
})
export class FavouriteService {

  constructor(private http:HttpClient){}
  url:string=environment.apiURl
  
  addFavourites(fav:FavoriteAddViewModel){
      return this.http.post<ResultViewModel>(this.url+"Favorite/Addapi",fav);
  }
  getFavs(id:string){
      return this.http.get<ResultViewModel>( environment.apiURl+"Favorite/Getfororder?userid="+id);
  }

  // getCartByUser(id:string){
  //     return this.http.get<ResultViewModel>( environment.apiURl+"Cart/get?id="+id)
  // }
  
  deleteFavs(id:number){
      return this.http.delete<ResultViewModel>(this.url+ "Favorite/Deleteapi?id="+id);
  }


}
