
export class Product
{
    id:number= 0;
    name:string ='';
    price:number = 0.00;
    color:string = '';
    providerName:string='';
    categoryName:string='';
    quantity:number=0
    rating:number=0
    description:string='';
    imgs:Img[] = [];

}


// export class ProductViewModel
// {
//     id:number= 0;
//     name:string ='';
//     price:number = 0.00;
//     color:string = '';
//     providerName:string='';
//     categoryName:string='';
//     description:string='';
//     imgs:Img[] = [];

    
// }



class Img
{
    id:number = 0 ;
   img:string = '' ;
}