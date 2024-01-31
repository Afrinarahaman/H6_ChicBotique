import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators'; // Import the map operator
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Category} from '../_models/category';



@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = environment.apiUrl + '/category';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

constructor(private http: HttpClient) { }

getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.apiUrl);
}

getCategoryCount(): Observable<number> {
  // getAllCategories function is to count the items in the array
  return this.getAllCategories().pipe(
    map(categories => categories.length)
  );
}

getCategory(categoryId: number): Observable<Category> {
    return this.http.get<Category>(`${this.apiUrl}/${categoryId}`);
}
getCategoriesWithoutProducts(): Observable<Category[]>{
  return this.http.get<Category[]>(`${this.apiUrl}/WithoutProducts`)
}

addCategory(category: Category): Observable<Category> {
  return this.http.post<Category>(this.apiUrl, category, this.httpOptions);
}


editCategory(categoryId: number, category: Category): Observable<Category> {
  return this.http.put<Category>(`${this.apiUrl}/${categoryId}`, category, this.httpOptions);
}

deleteCategory(authorId: number): Observable<Category> {
  return this.http.delete<Category>(`${this.apiUrl}/${authorId}`, this.httpOptions);

}
}
