import { CartItem } from './../_models/cartItem';
import { Component, OnInit } from '@angular/core';
import { CartService } from '../_services/cart.service';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { Role } from '../_models/role';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  public quantity: number = 1;  // Initial quantity
  public grandTotal: number = 0;
  public cartProducts: CartItem[] = [];  //property


  constructor(private cartService: CartService, private router: Router,private authService: AuthService) { }

  ngOnInit(): void {
    this.cartProducts = this.cartService.getBasket();
   this.grandTotal = this.calculateGrandTotal();
  }

  calculateGrandTotal(): number {
    let grandTotal = 0 ;
    for (let item of this.cartProducts) {
      grandTotal += item.productPrice * item.quantity;
    }
    return grandTotal;
  }



  async createOrder() {
    // let customerId=parseInt(this.authService.currentCustomerValue.id)

    if (this.authService.currentUserValue == null || this.authService.currentUserValue.id == 0) {
    this.router.navigate(['checkout']);
    }
    else
    {
      this.router.navigate(['/shippinsdetails/']);
      //var result = await this.cartService.addOrder();
      //console.log('result', result);
             //this.cartService.clearBasket();
           //this.router.navigate(['/thankyou/']);
    }
  }

  public basket = this.cartService.basket;

  removeItem(productId: number) {
    console.log(productId);
    if (confirm("are you sure to remove?")) {
      this.cartService.removeItemFromBasket(productId);

    }

    window.location.reload();
  }
  emptycart() {
    if (confirm("are u sure to remove?"))
      this.cartService.clearBasket();
    window.location.reload();
  }


  // Method to increase the quantity
  increaseQuantity(item: any): void {
    item.quantity++;
   this.quantity= item.quantity;

  }

  // Method to decrease the quantity, ensuring it doesn't go below 1
  decreaseQuantity(item: any): void {
    if (item.quantity > 1) {
      item.quantity--;
      this.quantity= item.quantity;
    }

  }

}
