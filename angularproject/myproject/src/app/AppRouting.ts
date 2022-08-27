import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";

import { NotfoundComponent } from "./notfound/notfound.component";
import { ProductListComponent } from "./Product/product-list/product-list/product-list.component";
import { ProductDetailsComponent } from "./Product/productDetails/product-details/product-details.component";
import { CartComponent } from "./cart/cart.component";
import { ContactusComponent } from "./contactus/contactus.component";
import { OrderComponent } from "./Order/order/order.component";
import { AuthGuard } from "./auth.guard";
import { FavouriteComponent } from "./favourite/favourite.component";
import { AboutusComponent } from "./aboutus/aboutus/aboutus.component";
import { SearchComponent } from "./search/search.component";
import { ServicesListComponent } from "./services-list/services.component";
import { ServicesDetailsComponent } from "./services-details/services-details.component";
import { ProfileComponent } from "./profile/profile.component";
// import { paypal } from "creditcardpayments/creditCardPayments";

const myAppRoutes :Routes = [
    {path:"",component:HomeComponent},
    
    {path:"products/list/:id",component:ProductListComponent},
    {path:"products/list",component:ProductListComponent},
    
    {path:"Product/details/:id",component:ProductDetailsComponent},
    {path:"cart",component:CartComponent,canActivate:[AuthGuard]},
    {path:"order",component:OrderComponent,canActivate:[AuthGuard]},
    // {path:"**",component:NotfoundComponent},
    {
        path:"Auth",
        loadChildren:()=>import('../app/auth/auth.module').then(p => p.AuthModule)
    },
    {path:"Contact",component:ContactusComponent},
    {path:"Favs",component:FavouriteComponent},
    {path:"Aboutus",component:AboutusComponent},
    {path:"search",component:SearchComponent},
    {path:"Services",component:ServicesListComponent},
    {path:"ServicesDetails/:id",component:ServicesDetailsComponent},
    {path:"profile",component:ProfileComponent},
]
@NgModule({
    imports:[RouterModule.forRoot(myAppRoutes)],
    exports:[RouterModule],
    
})
export class AppRoutingModule{

}