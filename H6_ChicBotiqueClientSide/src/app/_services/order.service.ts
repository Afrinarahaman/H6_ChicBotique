import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Order } from '../_models/order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  public shippingdetails = new BehaviorSubject<any>([]);
  public transactionId = new BehaviorSubject<any>([]);
  public paymentStatus =new BehaviorSubject<any>([])

  public paymentMethod =new BehaviorSubject<any>([])
  apiUrl1 = environment.apiUrl + '/Order';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  constructor(private http: HttpClient) { }

  setAddressData(data: any) {
    this.shippingdetails.next(data);
 }

  getAddressData() {
    return this.shippingdetails.asObservable();
 }
 setTransactionId(data: any) {
  this.transactionId.next(data);
}

getTransactionId() {
  return this.transactionId.asObservable();
}

setPaymentStatus(data: any) {
  this.paymentStatus.next(data);
}

getPaymentStatus() {
  return this.paymentStatus.asObservable();
}
setPaymentMethod(data: any) {
  this.paymentMethod.next(data);
}

getPaymentMethod() {
  return this.paymentMethod.asObservable();
}


  storeOrder(newOrder: Order): Observable<Order> {
    // console.log("mandag");
    console.log("storeORder", newOrder);
    console.log(this.apiUrl1);
    // return this.http.get<Order[]>(this.apiUrl);
   return this.http.post<Order>(this.apiUrl1, newOrder, this.httpOptions);
  }

  getOrderDetailsByOrderId(orderId:number):Observable<any>{

  return this.http.get<Order>(`${this.apiUrl1}/${orderId}`, this.httpOptions);
  }
}
