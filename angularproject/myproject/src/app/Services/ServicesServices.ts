import { HttpClient , HttpHeaders} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment"; 
import { ResultViewModel } from "../models/ResultViewModel";

 
@Injectable({ 
  providedIn: 'root' 
}) 
export class ServicesService { 
 
  constructor(private http:HttpClient) { } 
 
  public Get(PageIndex:number,PageSize:number) 
  { 
    return  this.http.get<ResultViewModel>(`https://localhost:50001/Services/GetServicesAPI?PageIndex=${PageIndex}&PageSize=${PageSize}`); 
  } 
 
   
}