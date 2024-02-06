import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { CartService } from 'src/app/_services/cart.service';
import { OrderService } from 'src/app/_services/order.service';
import { ProductService } from 'src/app/_services/product.service';

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
    private productService: ProductService) { }

  ngOnInit(): void {
  

    }
   
  
  async submitShippingForm()
  {
    

    console.log(this.shippingdetails);
    const addressData=this.shippingdetails
    this.orderService.setAddressData(addressData);
    
   this.router.navigate(['/payment']); 
   

    
  }

}
