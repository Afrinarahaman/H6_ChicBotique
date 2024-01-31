import { Order } from './../_models/order';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { BehaviorSubject, Observable, forkJoin, switchMap } from 'rxjs';
import { Guid } from 'guid-typescript';
import { map,of } from 'rxjs';
import { OrderService } from './order.service';
import { ShippingDetails } from '../_models/shippingdetails';
import { Token } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiUrl = environment.apiUrl + '/User';
  apiUrl1 = environment.apiUrl + '/User/register';
  apiUrl2 = environment.apiUrl + '/User/guestRegister';
  apiUrl3 = environment.apiUrl + '/AccountInfo';
  apiUrl4= environment.apiUrl+ '/User/changepassword';


  httpOptions = {
    headers: new HttpHeaders({  'Content-Type': 'application/json' }),
  };

  constructor(
    private http: HttpClient,
    private orderService:OrderService
  ) { }
//BehaviorSubject  to manage the user role, allowing components to subscribe and receive role updates.
  public getRole: BehaviorSubject<number> = new BehaviorSubject(0);
  public getRole$: Observable<number> = this.getRole.asObservable();

//Retrieves the list of all users.
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl)
  }
  getRole_(roleNr: number) {
    // This function can be used to notify subscribers about the role number
    this.getRole.next(roleNr)
  }
  getMembersCount(): Observable<number> {
    // getUsers function to count the items in the array
    return this.getUsers().pipe(
      map(users => users.filter(user => user.role === 1).length)
    );
  }
  getGuestsCount(): Observable<number> {
    // getUsers function to count the items in the array
    return this.getUsers().pipe(
      map(users => users.filter(user => user.role === 2).length)

    );
  }
 // Retrieves a list of users with the role of administrators.
  getAdminsList(): Observable<User[]> {
    return this.getUsers().pipe(
      map(users => users.filter(user => user.role === 0))
    );
  }


  getMembersList(): Observable<User[]> {
    return this.getUsers().pipe(
      map(users => users.filter(user => user.role === 1))
    );
  }

  getGuestsList(): Observable<User[]> {
    return this.getUsers().pipe(
      map(users => users.filter(user => user.role === 2))
    );
  }


  addUser(user: User): Observable<User> {
    return this.http.post<User>(this.apiUrl1, user, this.httpOptions);
  }
  guest_register(guestUser: User): Observable<User>{
    return this.http.post<User>(this.apiUrl2, guestUser, this.httpOptions);
  }
  getUserGuid(userId: number): Observable<string> {
    return this.http.get<string>(`${this.apiUrl3}/${userId}`);
  }
  getUserbyEmail(email:string):Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${email}`);;
  }

  registerUser(user: User): Observable<User>{
    return this.http.post<User>(this.apiUrl1, user, this.httpOptions);
  }


  updateUser(userId: number, user:User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${userId}`, user, this.httpOptions);
  }

  deleteUser(userId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${userId}`, this.httpOptions);
  }


  resetPassword(newPassword: string, userId: number): Observable<string> {
    const passwordEntityRequest = { Password: newPassword, UserId: userId };
    return this.http.post(`${this.apiUrl4}`, passwordEntityRequest, {
      ...this.httpOptions,
      responseType: 'text',  // Set the response type to 'text'
    });
  }





}
