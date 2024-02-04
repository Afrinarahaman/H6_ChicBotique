import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  apiUrl1 = environment.apiUrl + '/Payment/authorization';


  
  httpOptions = {
    headers:
      new HttpHeaders({
        'Content-Type': 'application/json',
        
      }),
  };
  constructor(private http: HttpClient) { }
}
