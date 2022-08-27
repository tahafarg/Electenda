import { Component, Input, OnInit } from '@angular/core';
import { Product } from 'src/app/models/Product';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  
  @Input() prod:Product = new Product()

  constructor() { }

  ngOnInit(): void {   
    console.log(this.prod)                   
  }

}
