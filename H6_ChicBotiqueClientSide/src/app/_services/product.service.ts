import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Product } from '../_models/product';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'; // Import the map operator

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl_product = environment.apiUrl +  '/product';
  private apiUrl_stock =environment.apiUrl + '/stock';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  constructor(private http : HttpClient) { }


  //Method for getting all products
  getAllProducts():Observable <Product[]>{
    return this.http.get<Product[]> (this.apiUrl_product);
  }
  getProductById(productId:number): Observable<Product>{
    return this.http.get<Product> (`${this.apiUrl_product}/${productId}`);
  }
  getProductsByCategoryId(categoryId:number): Observable<Product[]>{
    return this.http.get<Product[]>(`${this.apiUrl_product}/category/${categoryId} `)
  }

  getProductCount(): Observable<number> {
    // getAllProducts function to count the items in the array
    return this.getAllProducts().pipe(
      map(products => products.length)
    );
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl_product, product, this.httpOptions);
  }

  updateProduct(productId: number, product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl_product}/${productId}`, product, this.httpOptions);
  }

  deleteProduct(productId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl_product}/${productId}`, this.httpOptions);
  }

  getAvailableStock(productId: number): Observable<number> {
    return this.http.get<number>(`${this.apiUrl_stock}/${productId}`, this.httpOptions);
  }

}
