import { Component, OnInit } from '@angular/core';
import { Product } from '../models/Product';
import { SearchService } from './search.service';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
 
data : Product[]=[];
pageIndex: number = 1; //current page number
count: number = 1; //total pages
pageSize: number = 3;


  constructor(private search: SearchService)
   { 

   }

  ngOnInit(): void {
    // this.search.SearchData(text:string).subscribe(
    //   res => {
    //     if(res.success == true)
    //     {
    //     this.data = res.data as Product[]
    //     }
    //   })
   // this.GetProducts();
  }


  Search(text:string)
  {
    
     this.search.SearchData(text).subscribe(
      res => {
        if(res.success == true)
        {
        this.data = res.data.data as Product[]
        console.log(res.data.data)
        }
      })

  }

  // GetProducts(text:string){
  //   this.search.Get(text,this.pageIndex, this.pageSize).subscribe(
  //     res => {
  //       if(res.success)
  //       {
  
  //       this.count = res.data.count;
  //       this.pageIndex = res.data.pageIndex;
  //       this.pageSize = res.data.pageSize;
  //       this.data = res.data.data as Product[]
  
        
  //       }
  //     }
  
  //   )

  // }

  // onTableDataChange(event: any) {
  //   console.log(event);
  //   this.pageIndex = event;
  //   this.GetProducts()
  // }



}
