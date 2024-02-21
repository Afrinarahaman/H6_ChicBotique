import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { CartService } from 'src/app/_services/cart.service';
import { OrderService } from 'src/app/_services/order.service';
import { ProductService } from 'src/app/_services/product.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-shippingdetails',
  templateUrl: './shippingdetails.component.html',
  styleUrls: ['./shippingdetails.component.css']
})
export class ShippingdetailsComponent implements OnInit {

 
  
  public shippingdetails: any = {};
  id: any;
  constructor( private orderService: OrderService,
    private cartService: CartService, private router: Router,
    private productService: ProductService,
    private userService:UserService,
    private authService:AuthService) { }

  ngOnInit(): void {
  

    }
   
    useNewShippingDetails: boolean = false;

    submitShippingDetails(): void {
      if (this.useNewShippingDetails) {
        // Form submitted with new shipping details
        console.log('Form submitted with new shipping details');
        // You can access form values via ngForm reference shippingForm
      } else {
        // Use home address, no need to submit form
        console.log('Use home address');
      }
    }
  async submitShippingForm()
  {
    if (this.useNewShippingDetails) {

    console.log(this.shippingdetails);
    const addressData=this.shippingdetails
    this.orderService.setAddressData(addressData);
    
   this.router.navigate(['/payment']); 
    }
    else
    {
     var email=  sessionStorage.getItem('guestEmail');
      
      const addressData= this.userService.getUserbyEmail(email);
      this.orderService.setAddressData(addressData);
      this.router.navigate(['/payment']); */
    }
   

    
  }

}
