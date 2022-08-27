import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ResultViewModel } from '../models/ResultViewModel';

@Injectable({
  providedIn: 'root'
})
export class SearchService {



  constructor(private http:HttpClient) { }

  // public SearchData(name:string)
  // {
   
  // return  this.http.get<ResultViewModel>(`https://localhost:50001/Product/Get?name=${name}`);
      
  // }

  public SearchData(name:string ="", providerName:string = "", categoryName:string = "", price: number = 0) 
  { 
    
   return  this.http.get<ResultViewModel>(`https://localhost:50001/Product/Get?name=${name}&price=${price}&categoryName=${categoryName}&providerName=${providerName}`); 

  }

  public Get(name:string ="",PageIndex:number,PageSize:number) 
  { 
    return  this.http.get<ResultViewModel>(`https://localhost:50001/Product/Get?name=${name}&PageIndex=${PageIndex}&PageSize=${PageSize}`); 
  } 
 

  
}
