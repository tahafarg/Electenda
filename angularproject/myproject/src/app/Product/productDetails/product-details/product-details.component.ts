import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {  CartAddViewModel } from 'src/app/models/Cart';
import { Product } from 'src/app/models/Product';
import { ProductServices } from 'src/app/Services/ProductServices';
import { CartServices } from 'src/app/Services/CartServices';
import { FavoriteAddViewModel } from 'src/app/models/Favourite';
import { FavouriteService } from 'src/app/Services/FavouriteServices';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  
  prod:Product = new Product;
  loading:boolean=true;


  constructor(private prodService:ProductServices, private route:ActivatedRoute 
    , private cartSer:CartServices , private favSer:FavouriteService) { }

  ngOnInit(): void {

this.loading = true;
    let id:any;
    
    id= this.route.snapshot.params['id'];
    console.log(id)
    this.prodService.getProductById(id).subscribe(res=>{ 
      this.prod = res.data
          
      console.log(this.prod)
    //  console.log(res.data)
      console.log(this.prod.providerName)
    }
      )

   

    // toBeAddToCart(id:number){
    //   this.Add.emit(id);
    // }
  }

 AddC()
 {
  let u = localStorage.getItem('userId')
  console.log('uid ='+u)
   let sendCart = new CartAddViewModel

    sendCart.UserID = localStorage.getItem('userId')??""

  //  sendCart.name = this.prod.name
  //  sendCart.price = this.prod.price
   sendCart.ServicesId = null
   sendCart.productID = this.prod.id
   sendCart.quantity = 1
   
  console.log(sendCart)
  this.cartSer.addCart(sendCart).subscribe(res=>{
   // window.alert('Your product has been added to the cart!');
    window.alert(res.message)
  });
 }

//  AddF()
//  {
// //  console.log('uid ='+u)
//    let sendFav = new FavouriteAddViewModel();

//     sendFav.UserID = localStorage.getItem('userId')??"";
//     console.log(localStorage.getItem('userId'));

  
//    sendFav.ProductID = this.prod.id;
//    console.log(sendFav);
//    this.favSer.addFavourites(sendFav).subscribe(res=>{
//   window.alert('Your product has been added to the Favourites!');
//   });
  
//  }
AddF()
{
 let u = localStorage.getItem('userId')
 console.log('uid ='+u)
  let sendCart = new FavoriteAddViewModel;

   sendCart.UserID = localStorage.getItem('userId')??"";

 //  sendCart.name = this.prod.name
 //  sendCart.price = this.prod.price
  sendCart.ServicesId = null
  sendCart.ProductID = this.prod.id
  //sendCart.quantity = 1
  
 console.log(sendCart)
 this.favSer.addFavourites(sendCart).subscribe(res=>{
 //  window.alert('Your product has been added to the fav!');
    window.alert(res.message)
 });
}

}
