import { User } from './../../_models/user';
import { Component, OnInit } from '@angular/core';
import { UserService} from 'src/app/_services/user.service';
import { HttpClient } from '@angular/common/http';
import Swal from 'sweetalert2'
import { FormBuilder } from '@angular/forms';
import{Role} from 'src/app/_models/role';
import { Observable,map, of, tap } from 'rxjs';
import { SearchService } from 'src/app/_services/search.service';  // SearchService



@Component({
  selector: 'app-admin-user',
  templateUrl: './admin-user.component.html',
  styleUrls: ['./admin-user.component.css']
})
export class AdminUserComponent implements OnInit {
 selectedValue = '0';
  users: User[]= [];
  admins: User[] = [];
  members: User[] = [];
  guests: User[] = [];
  userId = 1; // replace with the desired user ID

  // Declare members$ here
  users$!: Observable<User[]>;
  searchTerm: string = '';
  searched: boolean = false; // Initialize searched to false

  message: string = '';
  roles!: Role


  user: User = {
    id: 0, email: '', firstName: '', lastName: '', password: '', address: '',
    city: '', postalcode: '', country: '', telephone: '', role: 0}


  constructor(private userService: UserService,
    private http: HttpClient,
    private formBuilder: FormBuilder,
    private searchService: SearchService) {
       // Fetch All Users list by default when the component is initialized
    this.users$ = this.userService.getUsers();

    }

  ngOnInit(): void {
    this.userService.getUsers().subscribe(
      u => {this.users = u
        console.log(u);});

    // Subscribe to changes in the search term
    this.searchService.search$.subscribe((term) => {
      // Update the local searchTerm whenever the search term changes
      this.searchTerm = term;
      //  call the searchProducts() method.
      this.searchUser();
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
/* searchUser() {

  if (this.searchTerm == null || this.searchTerm == '' && (onkeyup)) {
    alert('The search field is empty');
    this.fetchUsers();

  } else if (this.searchTerm.length >= 0) {
    this.userService.getUsers().subscribe((categories: User[]) => {
      // Use the filter method to filter categories based on the searchTerm
      this.users = this.users.filter((user) => {
        // Check if the category name contains the searchTerm (case-insensitive)
        const searchTerm = this.searchTerm.toLowerCase();
        return (
          user.firstName.toLowerCase().includes(searchTerm)

        );
      });
      console.log(this.searchTerm);

      // Check if no results were found
      if (this.users.length === 0) {
        alert('No results found');
      }

    });
  }
}*/

// Method to search specific user
searchUser(): void {
  if (!this.searchTerm.trim()) {
    alert('The search field is empty');
    this.fetchUsers();
  } else {
    this.users$.subscribe((users: User[]) => {
      // Use a temporary array to store the filtered users
      const filteredUsers = users.filter((user) => {
        // Check if the user's first name contains the searchTerm (case-insensitive)
        return user.firstName.toLowerCase().includes(this.searchTerm.toLowerCase());
      });

      // Update the main users array with the filtered results
      this.users = filteredUsers;
        // Set searched to true after the search is performed
    this.searched = true;

      // Check if no results were found
      if (filteredUsers.length === 0) {
        alert('No results found');
      }
    });
  }
}

 // Function to get role name based on role number
 getRoleName(roleNumber: number): string {
  switch (roleNumber) {
      case 0:
          return 'Administrator';
      case 1:
          return 'Member';
      case 2:
          return 'Guest';
      default:
          return 'Unknown';
  }
}

  // Method to handle role change
  onRoleChange(event:any): void {
  this.selectedValue=event.target.value;
  console.log('Selected Role:', this.selectedValue);

 // Fetch user information based on the selected role
  switch (this.selectedValue) {
    case '0':
      console.log('Fetching Users');
      // Assign the observable to members$
      this.users$ = this.userService.getUsers();
      break;


      case '1':
        console.log('Fetching Admins');
        // Assign the observable to members$
        this.users$ = this.userService.getAdminsList();
        break;

      case '2':
        console.log('Fetching Members');
        // Assign the observable to members$
        this.users$ = this.userService.getMembersList();
        break;

      case '3':
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


  // Assuming that the user has a property named 'userId' that corresponds to the 'id' in HomeAddress
  this.users$.subscribe((users) => {
    const userIds = users.map(user => user.id);

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



