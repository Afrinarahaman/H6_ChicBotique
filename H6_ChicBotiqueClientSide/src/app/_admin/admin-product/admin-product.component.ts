import { CategoryService } from 'src/app/_services/category.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Category, Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';
import Swal from 'sweetalert2';
import { CurrencyPipe } from '@angular/common';


@Component({
  selector: 'app-admin-product',
  templateUrl: './admin-product.component.html',
  styleUrls: ['./admin-product.component.css']
})
export class AdminProductComponent implements OnInit {

  productForm!:FormGroup;
  products :Product[]= [];
  categories: Category[] = [];

  category: Category = { id: 0, categoryName: '' }
  product: Product ={ id:0, title:"", price:0, description: "", image:"", stock:0, categoryId:0, category:this.category}

  message!: string;




  constructor(private productService:ProductService, private categoryService: CategoryService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(p => this.products = p);
    this.categoryService.getAllCategories().subscribe(c=>this.categories=c);
  }

  onFileChange(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      const file = event.target.files[0]; //selects the first file in the list
      this.product.image = file.name; // this line assigns the name of the selected file (file.name) to the image property of the product object
    }}

  createProductForm(){
    this.productForm=this.fb.group({
      id: 0,
      title: [''],
      price: 0,
      description: [''],
      image:[''],
      stock:0,
      categoryId:0
    })
  }
  cancel(): void {
    this.product = { id:0, title:"", price:0, description: "", image:"", stock:0, categoryId:0,
    category: this.category }
  }


  edit(product: Product): void {
    this.message = '';
    this.product = product;
    this.product.id = product.id || 0;
    console.log(this.product);
  }
  delete(product: Product): void {
    if (confirm('Delete product: '+ product.title+'?')) {
      this.productService.deleteProduct(product.id)
      .subscribe(() => {
        this.products = this.products.filter(pus => pus.id != product.id)
      })
    }
  }
  save(): void {
    console.log(this.product)
    this.message = '';

    if(this.product.id == 0) {
      this.productService.addProduct(this.product)
      .subscribe({
        next: (x) => {
          console.log(x);
          this.products.push(x);
          this.product = { id:0, title:"", price:0, description: "", image:"", stock:0,  categoryId: 0, category: this.category}
          this.message = '';
          Swal.fire({
            title: 'Success!',
            text: 'Product added successfully',
            icon: 'success',
            confirmButtonText: 'OK'
          });
        },
        error: (err) => {
          console.log(err.error);
          this.message = Object.values(err.error.errors).join(", ");
        }
      });
    } else {
      this.productService.updateProduct(this.product.id, this.product)
      .subscribe({
        error: (err) => {
          console.log(err.error);
          this.message = Object.values(err.error.errors).join(", ");
        },
        complete: () => {
          this.message = '';
          this.product = { id:0, title:"", price:0, description: "", image:"", stock:0, categoryId:0, category:this.category}
          Swal.fire({
            title: 'Success!',
            text: 'Product updated successfully',
            icon: 'success',
            confirmButtonText: 'OK'
          });
        }
      });
    }
  }

}
