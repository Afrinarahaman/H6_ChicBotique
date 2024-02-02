import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CategoryProductsComponent } from './category-products/category-products.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { CartComponent } from './cart/cart.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
//import { AdminCategoryComponent } from './_admin/admin-category/admin-category.component';
import { AdminUserComponent } from './_admin/admin-user/admin-user.component';
import { FormsModule } from '@angular/forms';
import { AdminProductComponent } from './_admin/admin-product/admin-product.component';
import{ AdminPanelComponent } from './_admin/admin-panel/admin-panel.component';



const routes: Routes = [
  { path: '', component: NavbarComponent },
  { path: 'frontpage', component: FrontpageComponent},
  { path: 'category_products/:id', component: CategoryProductsComponent },
  { path: 'product_details/:id', component: ProductDetailsComponent},
  { path: 'cart', component: CartComponent },
  {path:'wishlist',component:WishlistComponent},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'adminpanel', component: AdminPanelComponent, canActivate: [AuthGuard] },
 // { path: 'admin/category', component: AdminCategoryComponent,  canActivate: [AuthGuard]  },
  { path: 'admin/product', component: AdminProductComponent,  canActivate: [AuthGuard]  },
  { path: 'admin/user', component: AdminUserComponent,  canActivate: [AuthGuard]  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
    FormsModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
