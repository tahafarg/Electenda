import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/models/Category';
import { Product } from 'src/app/models/Product';
import { ProductServices } from 'src/app/Services/ProductServices';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  
  loading:boolean=true;
products:Product[] = []
pageIndex: number = 1; //current page number 
    count: number = 1; //total pages 
  
   //number of elements to get form database per request 
   pageSize: number = 3;

    id:number=this.route.snapshot.params['id'];;   
    
  constructor(private prodserv:ProductServices , private route:ActivatedRoute) { }

  ngOnInit(): void {
   
    // this.show()
    this.getProducts()
   
  }

  getProducts()
  {
    this.loading = true;
    
    
    this.prodserv.getProductByCat(this.id,this.pageIndex,this.pageSize).subscribe(res=>{ 
      if(res.success==true){ 
      console.log(res.data.data)
      console.log(this.id)
      this.products = res.data.data as Product[] 
    this.count = res.data.count
    this.pageIndex = res.data.pageIndex
    this.pageSize = res.data.pageSize
  }
    })

    
  }

  // show()
  // {
  //   this.prodserv.getProductByCat2(this.id).subscribe(res=>{
  //     this.products = res.data.data as Product[];
  //    // console.log(res.data)
  //   })  
  // }


  onTableDataChange(event: any) { 
    console.log(event); 
    this.pageIndex = event; 
    this.getProducts()
  }


}
