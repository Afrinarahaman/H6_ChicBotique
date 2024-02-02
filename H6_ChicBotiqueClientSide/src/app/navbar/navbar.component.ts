import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryService } from '../_services/category.service';
import { Category } from '../_models/category';
import { CartService } from '../_services/cart.service';
import { User } from "../_models/user";
import { AuthService } from '../_services/auth.service';
import { Role } from '../_models/role';
import { UserService } from '../_services/user.service';
import { WishlistService } from '../_services/wishlist.service';
import { SearchService } from '../_services/search.service';
import { Product } from '../_models/product';
import { ProductService } from '../_services/product.service';
import { BehaviorSubject, Observable, Subject, observable } from 'rxjs';




@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  title = 'Webshop_H5-Client';
  categories: Category[] = [];
  category: Category = { id: 0, categoryName: "" };
  currentUser: User = {
    id: 0, firstName: '', lastName: '', email: '', password: '', role: 2,
    address: '',
    telephone: '',
    city: '',
    postalcode: '',
    country: ''
  };
  allRoles = Role;
  // currentUserRole = 2;
  categoryId: number = 0;
  public searchTerm: string = "";

  products: Product[]=[];

  /*private dataSub = new Subject();

  getData$(){
    return this.dataSub.asObservable();
  }

  setData(data:any) {
    this.dataSub.next(data);
  }*/
  public totalItem = this.cartService.getBasket().length;
  public WL_totalItem: number = this.wishlistService.getWishlist().length;

  showSearchResults: boolean = false;
  x: any;
  isAdmin = false;
  isHovered = false;

  constructor(
    private router: Router,
    private authService: AuthService,
    private productService:ProductService,
    private categoryService: CategoryService,
    private cartService: CartService,
    private userService: UserService,
    private wishlistService: WishlistService,
    private searchService: SearchService
  ) {
    this.searchService.search.subscribe((term) => {
      this.searchTerm = term;
      // Perform search or update your UI as needed when the search term changes
    });
   
  }

  ngOnInit(): void {

    this.categoryService.getCategoriesWithoutProducts().subscribe(x => this.categories = x);
    console.log('value received ');
    this.authService.currentUser.subscribe(x => {
      this.currentUser = x;
      //this.currentUserRole = x.role;
      //this.isAdmin = this.checkIfUserAdmin(x)
      this.router.navigate(['/']);
    });

    console.log("x:", this.currentUser.role);
  }

  public checkIfUserAdmin(user: User | null) {
    //check role type and return true or false
    if (user != null) {
      return user.role == Role.Admin;
    }

    else
      return false;
  }
  /// Seach Funtionlity //
  onSearch(event: any) {
    this.searchTerm = (event.target as HTMLInputElement).value;
    console.log(this.searchTerm);
    this.searchService.search.next(this.searchTerm);
  }

  /*
  public checkIfUserNotGuest(user:User | null) {
    //check role type and return true or false
    if (user != null) {
      return user.role == Role.Admin || user.role == Role.Member
    }
    else
    return false;
  }
  public checkIfUserMember(user:User | null) {
    //check role type and return true or false
    if (user != null) {
      return user.role == Role.Member
    }
    else
    return false;
  }*/





  logout() {
    if (confirm('Are you sure you want to log out?')) {
      this.userService.getRole_(2);
      // ask authentication service to perform logout
      this.authService.logout();

      // subscribe to the changes in currentUser, and load Home component
      this.authService.currentUser.subscribe(x => {
        this.currentUser = x;
        this.router.navigate(['login']);
      });
    }
    else {
      if (this.x === 0) {
        this.router.navigate(['admin']);
      }
      else {
        this.router.navigate(['/']);
      }
    }
  }

}
