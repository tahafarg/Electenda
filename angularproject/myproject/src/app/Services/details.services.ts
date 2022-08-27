
import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core'; 
import { ResultViewModel } from "../models/ResultViewModel";
 
 
@Injectable({ 
  providedIn: 'root' 
}) 
export class DetailsService { 
 
  constructor(private http: HttpClient) { } 
 
  public GetServiceDetails(id:any) 
  { 
   return  this.http.get<ResultViewModel>(`https://localhost:50001/Services/ServicesDetailsAPI?id=${id}`); 
  } 
}