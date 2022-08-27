import { Component, OnInit } from '@angular/core';
import { Product} from '../models/Product'
import { ProductServices } from '../Services/ProductServices';
import { Brand } from '../models/Brand';
import { BrandServices } from '../Services/BrandSetvices';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  // loading:boolean = true;
  products:Product[] = []
  flashDeals:Product[]=[]
  newIn:Product[]=[]
  collections:Product[]=[]
  recomended:Product[]=[]
  recomended2:Product[]=[]

  bestProducts: Product[]=[]

  brands:Brand[]=[]
  
  constructor(private prodserv:ProductServices , private brandserv:BrandServices) { }

  ngOnInit(): void {
    // this.loading = true;
    // console.log("uid =" +localStorage.getItem('userId'))
    
    this.show()
  }

  show()
  {
   
    
    this.brandserv.getBrand().subscribe(
      res=>{
        if(res.success)
        {
  
        this.flashDeals= res.data as Product[]
      }
    }
    )

    this.prodserv.getFlashDealstProduct(3).subscribe(
      res=>{
        if(res.success)
        {
          this.flashDeals= res.data.data as Product[]
                  // console.log(res)
        }
        
      }
    )     

    this.prodserv.getNewIntProduct(4).subscribe(
      res=>{
        if(res.success)
        {
        this.newIn= res.data.data as Product[]
                 
        }
      }
    ) 

    this.prodserv.getcollectionsProduct(5).subscribe(
      res=>{
        if(res.success)
        {
        this.collections= res.data.data as Product[]
             
        }
      }
    ) 

    this.prodserv.getRecomendedProduct(6).subscribe(
      res=>{

        if(res.success)
        {
        this.recomended = res.data.data as Product[]
               
        }
      }
    ) 

    
    this.prodserv.getRecomended2Product(3).subscribe(
      res=>{
        if(res.success)
        {
          this.recomended2 = res.data as Product[]
        }
        
      }
    ) 



  }


}
