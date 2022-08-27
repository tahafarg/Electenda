import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServicesDetailsViewModel } from '../models/services';
import { DetailsService } from '../Services/details.services';

@Component({
  selector: 'app-services-details',
  templateUrl: './services-details.component.html',
  styleUrls: ['./services-details.component.css']
})
export class ServicesDetailsComponent implements OnInit {
  data: ServicesDetailsViewModel= new ServicesDetailsViewModel; 
 
  constructor(private details: DetailsService, private acr: ActivatedRoute) { } 
 
  ngOnInit(): void { 
    let id:any; 
 
    id = this.acr.snapshot.paramMap.get("id") 
    //console.log(id) 
    //console.log( this.acr.snapshot.paramMap.get("id")) 
 
    this.details.GetServiceDetails(id).subscribe( 
      res => { 
       if(res.success) 
       { 
 
        this.data = res.data as ServicesDetailsViewModel 
         console.log(this.data); 
       } 
      } 
  
      ) 
  
  } 

}
