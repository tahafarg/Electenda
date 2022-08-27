import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from'@angular/common/http'
import { AppComponent } from './app.component';
import { CardComponent } from './Product/product-card/card/card.component';
import { FooterComponent } from './footer/footer.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { AppRoutingModule } from './AppRouting';


import { HomeComponent } from './home/home.component';
import { CartComponent } from './cart/cart.component';
import { ListComponent } from './list/list.component';
import { ProductDetailsComponent } from './Product/productDetails/product-details/product-details.component';
import { ProductListComponent } from './Product/product-list/product-list/product-list.component';
import { ProductServices } from './Services/ProductServices';
import { CartServices } from './Services/CartServices';
import { CatItemComponent } from './Category/cat-item/cat-item/cat-item.component';
import { OrderServices } from './Services/OrderServices';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { CategoryServices } from './Services/CategoryServices';
import { CommonModule } from '@angular/common';
import { ContactusComponent } from './contactus/contactus.component';
import { FavouriteComponent } from './favourite/favourite.component';
import { OrderComponent } from './Order/order/order.component';
import { SearchComponent } from './search/search.component';
import { FavouriteService } from './Services/FavouriteServices';
import { BrandServices } from './Services/BrandSetvices';
import { AboutusComponent } from './aboutus/aboutus/aboutus.component';
import { paypal } from 'creditcardpayments/creditCardPayments';
import { NgxPaginationModule } from 'ngx-pagination';
import { ServicesListComponent } from './services-list/services.component';
import { ServicesService } from './Services/ServicesServices';
import { ServicesDetailsComponent } from './services-details/services-details.component';
import { DetailsService } from './Services/details.services';
import { ProfileComponent } from './profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    
    FooterComponent,
    NotfoundComponent,
    CardComponent,
    HomeComponent,
    CartComponent,
    ListComponent,
    ProductDetailsComponent,
    ProductListComponent,
    CatItemComponent,
    NavBarComponent,
    ContactusComponent,
    FavouriteComponent,
    OrderComponent,
    SearchComponent,
    AboutusComponent,
    ServicesListComponent,
    ServicesDetailsComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    NgxPaginationModule
 //   paypal.Buttons.driver('angular2', ng.core)
   
],

  providers: [
  
    ProductServices,
    CartServices,
    OrderServices,
    CategoryServices,
    FavouriteService,
    BrandServices,
    ServicesService,
    DetailsService
    

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
