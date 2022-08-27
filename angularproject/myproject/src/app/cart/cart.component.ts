import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderServices } from '../Services/OrderServices';
import { CartServices } from '../Services/CartServices';
import { NgForOf } from '@angular/common';
import { CartAddViewModel } from '../models/Cart';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  loading:boolean=true;
   
  carts:CartAddViewModel[] = []
  cart:CartAddViewModel = new CartAddViewModel
  sum:number = 0.0000

  // @Output() ubd = new EventEmitter<number>();
  constructor( private route:ActivatedRoute , private cartSer: CartServices ,private ordSer:OrderServices) { }

  ngOnInit(): void {
    this.loading = true
    var uid = localStorage.getItem('userId')??""

    this.cartSer.getCarts(uid).subscribe(res=>{
      this.carts = res.data as CartAddViewModel[]
      console.log(this.carts[0].src)
      console.log(res.data)
     
      this.carts.forEach(element=>{
        this.sum+=element.price*element.quantity
   })
    });

  }

  Del(id:number)
  {
    
    this.loading = true;
        this.cartSer.deleteCart(id).subscribe()
        window.location.reload()
  }


// Ubd(id:number, qty:any, ProductID:any,ServicesId:any)
// {
//   var cart = new CartAddViewModel
//   cart.UserID = localStorage.getItem("userId")??""
//   cart.quantity = qty as number
//   cart.id = id
//   cart.ProductID = ProductID as number;
// console.log(cart)
//   this.cartSer.editCart(cart).subscribe()
// }



Ubd(id:number, qty:any)
{
  // this.cartSer.getcarttById(id).subscribe(res=>this.prod = res.data)
 
  this.cartSer.getByID(id).subscribe(res =>
    { 
    if(res.success) 
    { 
      this.cart = res.data
 //     console.log(res.data)
  //    console.log(this.cart.productID)

  this.cart.id = res.data.id
   this.cart.UserID = localStorage.getItem("userId")??"" 
  this.cart.quantity = qty as number
   this.cart.productID = res.data.productID
  console.log(this.cart)
    
  this.cartSer.editCart(this.cart).subscribe()
 // window.location.reload()
      
    }
  })

 
  // this.cart.id = id
  //  this.cart.UserID = localStorage.getItem("userId")??"" 
  // this.cart.quantity = qty as number
  // console.log(this.cart)
    
  // this.cartSer.editCart(this.cart).subscribe()
 // window.location.reload()

}



}
