import { CategoryProductsComponent } from './category-products/category-products.component';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { SortPipe } from './_pipes/sort.pipe';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { CartComponent } from './cart/cart.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AdminProductComponent } from './_admin/admin-product/admin-product.component';



@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    SortPipe,
    NavbarComponent,
    CategoryProductsComponent,
    ProductDetailsComponent,
    CartComponent,
    WishlistComponent,
    LoginComponent,
    RegisterComponent,
    AdminProductComponent,






  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    RouterModule
  ],
  providers: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA], // Add CUSTOM_ELEMENTS_SCHEMA
  bootstrap: [AppComponent]
})
export class AppModule { }
