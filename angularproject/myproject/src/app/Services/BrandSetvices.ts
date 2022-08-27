
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ResultViewModel } from "../models/ResultViewModel";
import { Brand  } from "../models/Brand";


@Injectable()
export class BrandServices
{
  
    constructor(private http:HttpClient){}
    url:string=environment.apiURl


    getBrand()
    {
        return this.http.get<ResultViewModel>(environment.apiURl + 'Brand/Getapi');
    }


}