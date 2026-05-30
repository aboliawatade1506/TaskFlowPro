import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {  FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';

import { AngularcoreserviceService } from '../angularcoreservice.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatFormFieldModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  hidePassword = true;

    loginForm!: FormGroup;

  constructor(
     private router: Router,
    private fb: FormBuilder,
    private ser: AngularcoreserviceService
  ) { }

  ngOnInit(): void {

    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      role: ['', Validators.required]
    });

  }

  login() {

  const loginData = {
    email: this.loginForm.value.email,
    password: this.loginForm.value.password
  };

  this.ser.login(loginData).subscribe({

   next: (res: any) => {

  localStorage.clear();

  localStorage.setItem('email', loginData.email);
  localStorage.setItem('role', res.role.trim());
  localStorage.setItem('fullName', res.fullName);
  localStorage.setItem(
    'profileImage',
    res.profileImage || 'assets/default.png'
  );

  console.log(
    'Role Saved = ',
    localStorage.getItem('role')
  );

  this.router.navigate(['/dashboard']);
},

    error: (err: any) => {
      console.log(err);
    }

  });

}

}