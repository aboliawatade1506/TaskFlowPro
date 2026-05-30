import { Component } from '@angular/core';
import { AngularcoreserviceService } from '../angularcoreservice.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelect, MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {

 profile: any = {};

 profileData = {
  currentPassword: '',
  newPassword: '',
  confirmPassword: ''
};

 hideCurrentPassword = true;
 hideNewPassword = true;
 hideConfirmPassword = true;
 fullName = '';
profileImage = '';

  constructor(private ser: AngularcoreserviceService) { }
ngOnInit(): void {

  const email = localStorage.getItem('email');

  if (email) {

    this.ser.getProfile(email).subscribe({

      next: (res: any) => {

        this.profile = res;

        if (this.profile.profileImage) {

          this.profile.profileImage =
            'assets/images/' + this.profile.profileImage;

        } else {

          this.profile.profileImage =
            'assets/default.png';
        }

        this.fullName =
          localStorage.getItem('fullName') || '';

        console.log(this.profile.profileImage);
      },

      error: (err: any) => {
        console.log(err);
      }

    });

  }

}
updateProfile() {

  if (this.profileData.newPassword !== this.profileData.confirmPassword) {

    Swal.fire({
      icon: 'error',
      title: 'Password Mismatch',
      text: 'New Password and Confirm Password must be same'
    });
      this.profileData = {
    currentPassword: '',
    newPassword: '',
    confirmPassword: ''
  };
    return;
  }

  const payload = {
    ...this.profile,
      profileImage: this.profile.profileImage, 
    currentPassword: this.profileData.currentPassword,
    newPassword: this.profileData.newPassword,
    confirmPassword: this.profileData.confirmPassword
  };

  console.log('PAYLOAD =>', payload);

  this.ser.updateProfile(payload).subscribe({
    next: () => {
      Swal.fire({
        icon: 'success',
        title: 'Success',
        text: 'Profile Updated Successfully'
        
      });
      this.profileData.currentPassword = '';
      this.profileData.newPassword = '';
      this.profileData.confirmPassword = '';
    },
   error: (err: any) => {

      console.log(err);

      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: err.error || 'Profile Update Failed'
      });

      this.profileData = {
        currentPassword: '',
        newPassword: '',
        confirmPassword: ''
      };
    }
  });
}
  onFileSelected(event: any): void {

  const file = event.target.files[0];

  if (!file) {
    return;
  }

  const reader = new FileReader();

  reader.onload = () => {

    this.profile.profileImage = reader.result;

    localStorage.setItem(
      'profileImage',
      reader.result as string
    );
  };

  reader.readAsDataURL(file);
}

}