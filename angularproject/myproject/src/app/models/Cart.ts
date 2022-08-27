


export class CartAddViewModel
{
   
    id:number = 0
    name:string = ''
    price:number = 0.00
    quantity:number = 0
    UserID:string = ''
    ServicesId:number|null = 0
    productID:number= 0
    src:string=''
    
}

export class CartEditViewModel
{
   
    id:number = 0
    name:string = ''
    price:number = 0.00
    quantity:number = 0
    UserID:string = ''
    servicesId:number|null = 0
    productId:number|null = 0
    img:string=''
    
}
export class Cart
{
   quantity:number=0
   name:string=''
   price:number=0.00
   img:string=''
}

