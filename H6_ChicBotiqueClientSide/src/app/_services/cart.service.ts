import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, firstValueFrom } from 'rxjs';

import { AuthService } from './auth.service';


import { User } from '../_models/user';
import { UserService } from './user.service';
import { CartItem } from '../_models/cartItem';


@Injectable({
  providedIn: 'root'
})
export class CartService {

  private basketName = "WebShopProjectBasket";
  public basket: CartItem[] = [];
  public search = new BehaviorSubject<string>("");
  public shippingAddressData: any;
  id:number=0;
  userId:number=0;
  email:any;
  user: Observable<User[]> | undefined;
  public userGuid : string="";
  public transactionID : string="";
  public paymentStatus : string="";
  constructor(private router: Router, private authService: AuthService,private userService:UserService)
    {
      //this.userGuid =Guid.create()
    }

 getBasket(): CartItem[] {
    this.basket = JSON.parse(localStorage.getItem(this.basketName) || "[]");
    return this.basket;
  }
  saveBasket(): void {
    localStorage.setItem(this.basketName, JSON.stringify(this.basket));
  }
  addToBasket(item: CartItem): void {
    this.getBasket();
    let productFound = false;

    if (!productFound) {
      this.basket.push(item);
    }
    this.saveBasket();
  }
  getTotalPrice(): number {       //This calculates total price of all of the cartitems
    this.getBasket();
    var grandTotal = 0;
    for (let i = 0; i < this.basket.length; i++) {
      grandTotal += this.basket[i].quantity * this.basket[i].productPrice;
    }
    this.saveBasket();
    return grandTotal;
  }

  clearBasket(): CartItem[] {
    this.getBasket();
    this.basket = [];
    this.saveBasket();
    return this.basket;
  }
  removeItemFromBasket(productId: number): void {
    this.getBasket();
    for (let i = 0; i < this.basket.length; i += 1) {
      if (this.basket[i].productId === productId) {

        this.basket.splice(i, 1);


      }
    }

    this.saveBasket();

  }
async addOrder(): Promise<any> {
   //this is for memeber
    if (this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0) {

      this.userId=this.authService.currentUserValue.id;
      console.log('USER ID:', this.userId);
       /*this.userService.getUserGuid(this.userId).subscribe(data => {
        this.userGuid = data;

        console.log('GUID VALUE:', this.userGuid);
      });*/
      /*this.userGuid = await  firstValueFrom( this.userService.getUserGuid(this.userId));
     console.log("User Guid to string", this.userGuid)
        this.orderService.getAddressData().subscribe((data) => {
          this.shippingAddressData = data;
        });
      let orderitem: Order = {           // this is an object which stores customer_id, all of the ordereditems details and date when these have been ordered
        accountId: this.userGuid,
        amount: this.getTotalPrice(),
        transactionId: await firstValueFrom(this.orderService.getTransactionId() ),
        paymentStatus:await firstValueFrom(this.orderService.getPaymentStatus()),
        shippingDetails: {
          address: this.shippingAddressData.address ,
          city: this.shippingAddressData.city,
          postalCode:this.shippingAddressData.postalcode ,
          country:this.shippingAddressData.country ,
          phone: this.shippingAddressData.phone,
        },

        orderDetails: this.basket,
      }
      console.log('UserID:', this.userId);
      console.log('GUID VALUE:', this.userGuid);
      console.log(orderitem);
      var result = await firstValueFrom(this.orderService.storeOrder(orderitem))//calling storeCartItem function for storing all of the ordereditems deatils into the database.
      return result;

    } else {  //this is for guest
      this.email = sessionStorage.getItem('guestEmail');

    var user=  await firstValueFrom(this.userService.getUserbyEmail(this.email))
    this.userId=user.id;
    this.userGuid = await  firstValueFrom( this.userService.getUserGuid(this.userId));
    this.transactionID= await firstValueFrom(this.orderService.getTransactionId() );
      this.paymentStatus= await firstValueFrom(this.orderService.getPaymentStatus());
      let orderitem: Order = {
       accountId:this.userGuid,
        orderDetails: this.basket,
        amount: this.getTotalPrice(),
        transactionId: this.transactionID,
        paymentStatus:this.paymentStatus,
        shippingDetails: {
          address:this.shippingAddressData.address ,
          city: this.shippingAddressData.city,

          postalCode:this.shippingAddressData.postalcode ,
          country:this.shippingAddressData.country ,
          phone: this.shippingAddressData.phone,

        }
      }

      var result = await firstValueFrom(this.orderService.storeOrder(orderitem));
      return result;


    }

  }*/
  

  


    
}
}
}
