import { ShippingDetails } from 'src/app/_models/shippingdetails';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { Router, ActivatedRoute } from '@angular/router';  // Add ActivatedRoute import
import Swal from 'sweetalert2';
import { AuthService } from 'src/app/_services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  currentUser: User = {
    id: 0,
    email: '',
    firstName: '',
    lastName: '',
    password: '',
    address: '',
    city: '',
    country: '',
    postalcode: '',
    telephone: '',
    role: 2
  };
  profileForm: FormGroup = this.formBuilder.group({}); // initialization

  constructor(
    private router: Router,
    private route: ActivatedRoute,  // Add ActivatedRoute to the constructor
    private authService: AuthService,
    private userService: UserService,
    private formBuilder: FormBuilder
  ) {
    this.authService.currentUser.subscribe(user => {
      if (user) {
        this.userService.getUsers().subscribe(userData => {
          // Assign the first user in the array to currentUser
          this.currentUser = userData[this.currentUser.id];
          console.log("User details are: ", this.currentUser);
          this.initializeForm(); // Initialize the form with user data
        });
      }
    });
  }

  ngOnInit(): void {}

  newUser(): User {
    return {
      id: 0,
      email: '',
      firstName: '',
      lastName: '',
      password: '',
      address: '',
      city: '',
      country: '',
      postalcode: '',
      telephone: '',
      role: 2
    };
  }

  initializeForm(): void {
    this.profileForm = this.formBuilder.group({
      firstName: [this.currentUser.firstName, Validators.required],
      lastName: [this.currentUser.lastName, Validators.required],
      email: [this.currentUser.email, [Validators.required, Validators.email]],
      address: [this.currentUser.address, Validators.required],
      phone: [this.currentUser.telephone, [Validators.required, Validators.email]],

    });
  }

  cancel(): void {
    this.profileForm.reset(); // Reset the form
  }

  save(): void {
    if (this.profileForm.valid) {
      const updatedUser: User = {
        ...this.currentUser,
        ...this.profileForm.value
      };

      this.userService.updateUser(updatedUser.id, updatedUser).subscribe({
        error: (err) => {
          console.log(err.error);
        },
        complete: () => {
          this.cancel(); // Reset the form after a successful update
          console.log('Profile updated successfully');
        }
      });
    }
  }




  // To change Password
  showChangePasswordForm = false;
  currentPassword: string = '';
  newPassword: string = '';
  confirmPassword: string = '';

  toggleChangePasswordForm() {
    this.showChangePasswordForm = !this.showChangePasswordForm;
  }


  changePassword(newPassword: string, userId: number) {

    this.userService.resetPassword(this.newPassword, userId).subscribe(
      (response: string) => {
        console.log('Password changed successfully', response);
        Swal.fire('Success', 'Password changed successfully', 'success');
        console.log("New password: ", newPassword);
      },
      (error) => {
        console.error('Password change failed', error);
        Swal.fire('Error', 'An error occurred while changing the password. Please try again.', 'error');
      }
    );

  }

  cancelChangePassword() {
    // Reset the form and hide the change password section
    this.resetChangePasswordForm();
  }

  resetChangePasswordForm() {
    this.currentPassword = '';
    this.newPassword = '';
    this.confirmPassword = '';
    this.showChangePasswordForm = false;
  }


}
