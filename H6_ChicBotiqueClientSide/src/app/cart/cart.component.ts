import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from '../_services/cart.service';
import { AuthService } from '../_services/auth.service';
import { CartItem } from '../_models/cartItem';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  public quantity: number = 0;//variable declaration for Productquantity

  public grandTotal: number = 0;// variable declaration for storing total amount of the purchase
  public cartProducts: CartItem[] = [];  //property
  public basket = this.cartService.basket; //getting basket from the service
  constructor(private cartService: CartService, private router: Router,private authService: AuthService) //dependency injection of different services
  { }

  ngOnInit(): void {
    this.cartProducts = this.cartService.getBasket(); //getting all chosen cartproducts of the customer
    this.grandTotal = this.cartService.getTotalPrice();//getting the total price of the items
  }
  async processOrder() //method is for processing order after clicking the BUY button
  {
 
 
    if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0) {
    this.router.navigate(['checkout']);
    }
    else
    {
      this.router.navigate(['/shippingdetails/']);
      
    }
      
  }
  removeItem(productId: number) {
    console.log(productId);
    if (confirm("are you sure to remove?")) {
      this.cartService.removeItemFromBasket(productId);
      this.cartProducts = this.cartService.getBasket();

    }
   
   
  }

  emptycart() {
    if (confirm("are u sure to remove?"))
      this.cartService.clearBasket();
   
      this.cartProducts=[];

  }
 

}
