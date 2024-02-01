import { NgModule} from '@angular/core';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { SortPipe } from './_pipes/sort.pipe';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './navbar/navbar.component';
import { FormsModule } from '@angular/forms';
import { CartComponent } from './cart/cart.component';
import { AdministratorComponent } from './_admin/administrator/administrator.component';
import { AdminProductComponent } from './_admin/admin-product/admin-product.component';
import { AdminUserComponent } from './_admin/admin-user/admin-user.component';

@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    SortPipe,
    NavbarComponent,
    CartComponent,
    AdministratorComponent,
    AdminProductComponent,
    AdminUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    [FormsModule]
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
