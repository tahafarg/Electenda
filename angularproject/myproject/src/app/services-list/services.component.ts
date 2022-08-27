import { Component, OnInit } from '@angular/core';
 import { ServicesViewModel } from '../models/services';
import { ServicesService } from '../Services/ServicesServices';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: ['./services.component.css']
})
export class ServicesListComponent implements OnInit {
   data: ServicesViewModel[] = [];
   pageIndex: number = 1; //current page number
    count: number = 1; //total pages
 
   //number of elements to get form database per request
   pageSize: number = 3;
  //  tableSizes: any = [1, 5, 10, 20];
  constructor(private service : ServicesService) {
   }

  ngOnInit(): void {
   this.GetServices();
  
  }

  GetServices(){
    this.service.Get(this.pageIndex, this.pageSize).subscribe(
      res => {
        if(res.success)
        {
  
        this.count = res.data.count;
        this.pageIndex = res.data.pageIndex;
        this.pageSize = res.data.pageSize;
        this.data = res.data.data as ServicesViewModel[]
  
         
        //  console.log(res);
        //  console.log(this.data)
        }
      }
  
    )

  }


  onTableDataChange(event: any) {
    console.log(event);
    this.pageIndex = event;
    this.GetServices();
  }
 




}
