import { Component, OnInit } from '@angular/core';
import { UserService} from 'src/app/_services/user.service';
import { HttpClient } from '@angular/common/http';
import Swal from 'sweetalert2'
import { FormBuilder } from '@angular/forms';
import{Role} from 'src/app/_models/role';
import{User} from 'src/app/_models/user';
import { Observable,map, of, tap } from 'rxjs';
import { SearchService } from 'src/app/_services/search.service';  // SearchService



@Component({
  selector: 'app-admin-user',
  templateUrl: './admin-user.component.html',
  styleUrls: ['./admin-user.component.css']
})
export class AdminUserComponent implements OnInit {
 selectedValue = '1';
  users: User[]= [];
  admins: User[] = [];
  members: User[] = [];
  guests: User[] = [];
  userId = 1; // replace with the desired user ID
  shippingAddresses: string[] = [];
  address: any;
  searchTerm: string = '';

 /// addresses: HomeAddress[]=[];
/*  homeAddress: HomeAddress = {
    accountId:'',
    id: 0,
    address: '',
    city: '',
    postalCode: '',
    country:'',
    phone:''
  }
*/

  user: User = {
    id: 0,email: '', firstName: '', lastName: '', password: '', address: '',
    city: '', postalcode: '', country: '', telephone: '', role: 0}




  message: string = '';
  roles!: Role
  p: any;
  // Declare members$ here
 users$!: Observable<User[]>;
// homeaddress$!: Observable<HomeAddress[]>;


  constructor(private userService: UserService,
    private http: HttpClient,
    private formBuilder: FormBuilder,
    private searchService: SearchService) {
       // Fetch Members list by default when the component is initialized
    this.users$ = this.userService.getMembersList();

    }

  ngOnInit(): void {
   // Set default selected value to '1' for Members
   this.selectedValue = '1';
   // Fetch Members list by default
   this.users$ = this.userService.getMembersList();
 //  this.homeaddress$=this.addressService.getAllAddress();
   /* Subscribe to the address observable*/
 // this.addressService.getAllAddress().subscribe(address =>{
   // console.log("Home Address is: ",this.homeAddress.id);
   // console.log("Home Address Details are: ",this.homeAddress);

    // Subscribe to changes in the search term
    this.searchService.search$.subscribe((term) => {
      // Update the local searchTerm whenever the search term changes
      this.searchTerm = term;
      //  call the searchProducts() method.
      this.searchUsers();
  });
}
// method to handle input changes in the search field
onSearchInputChange() {
  // Update the search term in the SearchService
  this.searchService.updateSearchTerm(this.searchTerm);
}

   // method ot fetch all users
fetchUsers() {
  this.userService.getUsers().subscribe((u) => (this.users = u));
}



//method to search specific user
  searchUsers() {

    if (this.searchTerm == null || this.searchTerm == '' && (onkeyup)) {
      alert('The search field is empty');
      this.fetchUsers();

    } else if (this.searchTerm.length >= 0) {
      this.userService.getUsers().subscribe((users: User[]) => {
        // Use the filter method to filter products based on the searchTerm
        const filteredUsers = users.filter((user) => {
          // Check if the data contains the searchTerm (case-insensitive)
          const searchTerm = this.searchTerm.toLowerCase();
          return (
           user.firstName.toLowerCase().includes(searchTerm) ||
           user.lastName.toLowerCase().includes(searchTerm) ||
           user.email.toLowerCase().includes(searchTerm) ||
           user.telephone.toLowerCase().includes(searchTerm)
          );
        });
        // Update the users$ observable with the filtered results
      this.users$ = of(filteredUsers);


        // Check if no results were found
        if (filteredUsers.length === 0) {
          alert('No results found');
        }



      });
    }
  }


  // Method to handle role change
  onRoleChange(event:any): void {
  this.selectedValue=event.target.value;
  console.log('Selected Role:', this.selectedValue);

 // Fetch user information based on the selected role
  switch (this.selectedValue) {
      case '0':
        console.log('Fetching Admins');
        // Assign the observable to members$
        this.users$ = this.userService.getAdminsList();
        break;

      case '1':
        console.log('Fetching Members');
        // Assign the observable to members$
        this.users$ = this.userService.getMembersList();
        break;

      case '2':
        console.log('Fetching Guests');
        // Assign the observable to members$
        this.users$ = this.userService.getGuestsList();
        break;

      default:
        console.error('Unexpected selectedValue:', this.selectedValue);
         // Set default to Members in case of unexpected value
         this.selectedValue = '1';
         this.users$ = this.userService.getMembersList();
        break;
    }
     // Now, also fetch and print the address
/*this.addressService.getAllAddress().subscribe((allAddresses) => {
  console.log('All Addresses on Role Change:', allAddresses);*/

  // Assuming that the user has a property named 'userId' that corresponds to the 'id' in HomeAddress
  this.users$.subscribe((users) => {
    const userIds = users.map(user => user.id);

    // Fetch addresses for specific users based on their IDs
  /*  this.addressService.getAddressByIds(userIds).subscribe((userAddresses) => {
      console.log('Addresses for Users on Role Change:', userAddresses);

      // Combine userAddresses with allAddresses as needed
      // For example, you can merge the arrays or create a new array
      const combinedAddresses: HomeAddress[] = allAddresses.concat(userAddresses);


      // Assign the combined addresses to homeaddress$
      this.homeaddress$ = of(combinedAddresses);*/
    });



  }






  edit_member(user: User): void {
    this.message = '';
    this.user = user;
    this.user.id = user.id || 0;
    console.log(this.user);
  }


  delete_member(user: User): void {
    if (confirm('Delete user: '+user.firstName+' '+ user.lastName+'?')) {
      this.userService.deleteUser(user.id)
      .subscribe(() => {
        this.users = this.users.filter(cus => cus.id != user.id)
      })
    }
  }




  cancel(): void {
    this.user =  { id: 0,email: '', firstName: '', lastName: '', password: '', address: '',
    city: '', postalcode: '', country: '', telephone: '', role: 1};
  }






}



