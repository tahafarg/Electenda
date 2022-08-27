import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Category } from 'src/app/models/Category';
import { CategoryServices } from 'src/app/Services/CategoryServices';
import { Subject } from "rxjs";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cat-item',
  templateUrl: './cat-item.component.html',
  styleUrls: ['./cat-item.component.css']

})
export class CatItemComponent implements OnInit {

  @Input() cat:Category = new Category
  @Output() chan = new EventEmitter<number>()
  catt:Subject<Category> = new Subject<Category>();
  constructor(private catSer:CategoryServices,private route:ActivatedRoute) { }

  ngOnInit(): void {
    
   
    
  }
   change(id:number){
     
    this.chan.emit(id);
   }
}
