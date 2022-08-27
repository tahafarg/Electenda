import { Component, OnInit } from '@angular/core';
import { OrderServices } from 'src/app/Services/OrderServices';
import { render } from 'creditcardpayments/creditCardPayments'
import { CartServices } from 'src/app/Services/CartServices';
import { CartAddViewModel } from 'src/app/models/Cart';
import { ActivatedRoute } from '@angular/router';
import { elementAt } from 'rxjs';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {

  loading:boolean=true;
  carts:CartAddViewModel[] = []
  cart:CartAddViewModel = new CartAddViewModel
  price:number=0.00
  sum:number = 0.000

  
  //total:string=""
  constructor(private route:ActivatedRoute , private cartSer: CartServices ,private ordSer:OrderServices) {
     
    //var x= this.carts[0].price.toString()
    //var sum =0.00
    //   for (let x =0 ; x<= this.carts.length ; x++)
    // {
    //   sum =this.carts[x].price
    // }
    //this.carts.forEach(element=>{
      //   sum+=element.price
    //})
    //console.log(sum)
    //var x =9
     
    var uid = localStorage.getItem('userId')??""

    this.cartSer.getCarts(uid).subscribe(res=>{
      this.carts = res.data as CartAddViewModel[]
      
    this.carts.forEach(element=>{
         this.sum+=element.price*element.quantity
    })
     //  console.log(this.sum)

    render(
      { 
     
      id:"#paypalBtn",
      currency:"UDC",
      value:this.sum.toString(),
  
      onApprove:(details)=>
      {
       
        
        this.ordSer.addOrder(localStorage.getItem('userId')??"").subscribe();
        alert("successful");
      }
      
     })

  
    });
    
    // render(
    //   { 
     
    //   id:"#paypalBtn",
    //   currency:"UDC",
    //   value:"9",
  
    //   onApprove:(details)=>
    //   {
       
        
    //     this.ordSer.addOrder(localStorage.getItem('userId')??"").subscribe();
    //     alert("successful");
    //   }
      
    //  })

     
   

    }

  ngOnInit(): void {

    this.loading = true
    
    var uid = localStorage.getItem('userId')??""

    this.cartSer.getCarts(uid).subscribe(res=>{
      this.carts = res.data as CartAddViewModel[]
      console.log(uid)
       //var sum =0.00
    //   for (let x =0 ; x<= this.carts.length ; x++)
    // {
    //   sum =this.carts[x].price
    // }
    // this.carts.forEach(element=>{
    //      this.sum+=element.price
    // })
   
    console.log(this.sum)
  //  console.log(this.carts[0].price)
    });  

  }

  Add()
  {
    var c= localStorage.getItem('userId')??"";
    console.log(c);
    this.ordSer.addOrder(localStorage.getItem('userId')??"").subscribe();
  }


}

