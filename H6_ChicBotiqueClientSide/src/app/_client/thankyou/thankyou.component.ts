import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CartItem } from 'src/app/_models/cartItem';
import { Order } from 'src/app/_models/order';
import { ShippingDetails } from 'src/app/_models/shippingdetails';
import { AuthService } from 'src/app/_services/auth.service';
import { OrderService } from 'src/app/_services/order.service';

@Component({
  selector: 'app-thankyou',
  templateUrl: './thankyou.component.html',
  styleUrls: ['./thankyou.component.css']
})
export class ThankyouComponent implements OnInit {

  public result:any;
  
  constructor(private orderService:OrderService, private authService:AuthService, private route:ActivatedRoute) { }
  isShown: boolean = false ;
  public order: Order = {
    id: 0,
    accountInfoId: " ",
    clientBasketId:'',
    orderDetails: [],
    shippingDetails: {
      address: '',
      city: '',

      postalCode: '',
      country: '',
      phone: ''
    },
    amount: 0,
    transactionId: '',
    status: '',
    paymentMethod:''
  } ;
  public orderDetails: Array<CartItem> = [];
  public shippingdetails: ShippingDetails = {address: '',
  city: '',

  postalCode: '',
  country: '',
  phone: ''}

  orderId:number=0; 
  localAccountId:string ="";
  ngOnInit(): void {
    
  }
  
   detail(){

    this.orderId = parseInt(this.route.snapshot.paramMap.get('orderId')||'0');
   
    this.orderService.getOrderDetailsByOrderId(this.orderId).subscribe(res => {
      this.order = res;

     
     if(res.id!=0)
     {
      this.isShown = ! this.isShown;
     }

    
  });
    
  }

}
