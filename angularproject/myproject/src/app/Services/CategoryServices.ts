
import { HttpClient } from "@angular/common/http";
import { Call } from "@angular/compiler";
import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { environment } from "src/environments/environment";
import { Category } from "../models/Category";
import { ResultViewModel } from "../models/ResultViewModel";

@Injectable()
export class CategoryServices
{
    catid:Subject<number> = new Subject<number>()
    constructor(private http:HttpClient){}
    url:string = environment.apiURl;
    
    getCat()
    {
        return this.http.get<ResultViewModel>(environment.apiURl + 'Category/getapi');
    }
    getCategoryforList(){

        return this.http.get<ResultViewModel>(environment.apiURl + 'Category/getCategoryforList');
    }

    getCatById(id:number)
    {
        return this.http.get<ResultViewModel>( environment.apiURl+"Category/getbyid?id="+id)
    }
    

}