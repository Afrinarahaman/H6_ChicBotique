import { CategoryService } from 'src/app/_services/category.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from '../../_services/product.service';
import { UserService } from 'src/app/_services/user.service';
import { CommonModule} from '@angular/common';




@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {
  currentDate: Date = new Date();
  formattedDate: string ="";
  greeting: string ="";
  sidebarCollapse = false;
  productCount: number = 0;
  categoryCount: number= 0;
  memberCount: number= 0;
  guestCount: number= 0;
  admin: any;
  products: any;


  toggleSidebar() {
    this.sidebarCollapse = !this.sidebarCollapse;
  }

  constructor(private router:Router,
    private productService: ProductService,
    private categoryService: CategoryService,
    private userService: UserService)
    {  // Format the date
      this.formattedDate = this.formatDate(this.currentDate);
      // Determine the time of the day for greeting
      this.greeting = this.getGreeting(this.currentDate);

    }

     formatDate(date: Date): string {
        const options: Intl.DateTimeFormatOptions = {
          day: 'numeric',
          month: 'long',
          year: 'numeric',
          hour12: false, // Use 24-hour format
          timeZone: 'Europe/Copenhagen', // Set the time zone to Copenhagen
        };

        return new Intl.DateTimeFormat('da-DK', options).format(date);
      }

      getGreeting(date: Date): string {
        const hours = date.getHours();

        if (hours >= 5 && hours < 12) {
          return 'Good Morning ';
        } else if (hours >= 12 && hours < 18) {
          return ' Good Afternoon';
        } else {
          return 'Good Evening';
        }
      }

  ngOnInit(): void {
    this.currentDate= new Date();

    //to count products
    this.productService.getProductCount().subscribe(count => {
        this.productCount = count;  });
    //to count categories
    this.categoryService.getCategoryCount().subscribe(count=>{
      this.categoryCount= count;});
    //to count members
    this.userService.getMembersCount().subscribe(count=>{
      this.memberCount= count;});
    //to count guests
    this.userService.getGuestsCount().subscribe(count=>{
      this.guestCount= count;});

    this.userService.getUsers().subscribe
  }


  navigateToAdminPage() {
    console.log('Clicked!');
    this.router.navigate(['/adminpanel']);
  }

  performSearch() {
    // Add your search logic here
  }



}
