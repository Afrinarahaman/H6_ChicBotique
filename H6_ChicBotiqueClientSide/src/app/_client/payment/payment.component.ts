import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ICreateOrderRequest, IPayPalConfig } from 'ngx-paypal';
import { Order } from 'src/app/_models/order';
import { CartService } from 'src/app/_services/cart.service';
import { OrderService } from 'src/app/_services/order.service';
import { PaymentService } from 'src/app/_services/payment.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  
  cartTotal =0;
 public payPalConfig?: IPayPalConfig;
  showSuccess!: any;
  order:Order = {
    id: 0,
    accountId: '',
    shippingDetails: {
      address: "",
      city: '',
      country: '',
      postalCode: '',
      phone: ''
    },

    orderDetails: [],
    amount: 0,
    transactionId: '',
    status: '',
    paymentMethod:''
  }

  //@ViewChild('paymentRef', {static : true}) paymentRef!: ElementRef;
  shippingdetails: any;
  id: any;
  trasactionId:  any;
 paymentStatus:any;
 paymentMethod:any;
  constructor(private router: Router, private cartService:CartService, 
    private orderService:OrderService, private paymentService: PaymentService) { }

  ngOnInit(): void {
    this.cartTotal= this.cartService.getTotalPrice();
    this.shippingdetails=this.orderService.getAddressData()
    
  localStorage.getItem('Cart Total') as any;
    console.log(this.cartTotal);
    console.log();
  
    this.initConfig();
  }

  private initConfig(): void {
    
 
      this.payPalConfig = {
        currency: 'DKK',
        clientId: `${environment.Client_Id}`,       
        createOrderOnClient: (data) =>

        //const addressData=this.shippingdetails;
   
          <ICreateOrderRequest>{
            
            intent: 'CAPTURE',
            purchase_units: [
              {
                amount: {
                  currency_code: 'DKK',
                  value: `${this.cartTotal}`,
                  
                }
                
              },
       
            ],
            
          },
       
       
        onApprove: (data, actions) => {
          console.log(
            'onApprove - transaction was approved',
            data,
            actions
          );
          },

      
        onClientAuthorization: (data) => {
          
        },
        onCancel: (data, actions) => {
          console.log('OnCancel', data, actions);
        },
        onError: (err) => {
          console.log('OnError', err);
        },
        
      };
    
    }
    cancel(){
      this.router.navigate(['cart']);
    }

}
