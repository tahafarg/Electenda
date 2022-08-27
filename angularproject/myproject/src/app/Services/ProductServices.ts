import { HttpClient , HttpHeaders} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ResultViewModel } from "../models/ResultViewModel";
import { Product } from "../models/Product";


@Injectable()
export class ProductServices
{

    constructor(private http:HttpClient){}
    url:string = environment.apiURl;
    
    getProduct()
    {
        return this.http.get<ResultViewModel>(environment.apiURl + 'Product/get');
    }

    getProductByCat2(id:number)
    {
        return this.http.get<ResultViewModel>(environment.apiURl + 'Product/get?catId='+id);
    }

    getProductByCat(id:number,pageIndex:number,pageSize:number)
    {
        return this.http.get<ResultViewModel>(`https://localhost:50001/Product/Get?catId=${id}&pageIndex=${pageIndex}&pageSize=${pageSize}`)
    }

   

    getProductById(id:number)
    {
        return this.http.get<ResultViewModel>( environment.apiURl+"Product/getbyid?id="+id)
    }
    getFlashDealstProduct(id:number)
    {
        return this.http.get<ResultViewModel>(`https://localhost:50001/Product/Get?catId=${id}}&pageSize=4`);
    }

    getNewIntProduct(id:number)
    {
        return this.http.get<ResultViewModel>(`https://localhost:50001/Product/Get?catId=${id}}&pageSize=3`);
    }

    getcollectionsProduct(id:number)
    {
        return this.http.get<ResultViewModel>(`https://localhost:50001/Product/Get?catId=${id}}&pageSize=3`);
    }


    getRecomendedProduct(id:number)
    {
        return this.http.get<ResultViewModel>(`https://localhost:50001/Product/Get?catId=${id}}&pageSize=4`);
    }

    getRecomended2Product(id:number)
    {
        return this.http.get<ResultViewModel>(`https://localhost:50001/Product/Get?catId=${id}}&pageSize=4`);
    }
    
    SearchProduct(pageIndex:number,pageSize:5)
    {
        return this.http.get<ResultViewModel>(`${environment.apiURl}Product/get?pageSize=${pageSize}&pageIndex=${pageIndex}`);
    }

    getlist(pageIndex:number,pageSize:number)
    {
        let headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'token': "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJQcm92aWRlciIsImV4cCI6MTY1OTc5OTE3NX0.keQQeREaaHpMdg2CJnOmXFmJOw5Ankk_SxFeMe4Qk0Y",
         });
        let options = { headers: headers };
        return this.http.get<ResultViewModel>(environment.apiURl + `Product/get?orderBy=ID&isAscending=False&pageIndex=${pageIndex}&pageSize=${pageSize}`,options);
    }
    
}