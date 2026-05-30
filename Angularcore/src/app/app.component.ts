import { Component } from '@angular/core';
import { RouterOutlet,Router,NavigationEnd,RouterLink   } from '@angular/router';
import {MatIconModule} from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AngularcoreserviceService } from './angularcoreservice.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
    RouterLink,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Angularcore';

  isLoginPage = false;
  adminName = '';
  searchText='';
  userName: string = '';
  role: string = '';

  constructor(private router: Router,  
    private ser: AngularcoreserviceService)
   {
    // first load
    this.checkLoginPage(this.router.url);

    // route change
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.checkLoginPage(event.urlAfterRedirects);
      }
    });
  }

  ngOnInit() {

  this.adminName = localStorage.getItem('fullName') || '';
  this.role = localStorage.getItem('role') || '';

  console.log("FullName =", this.adminName);
  console.log("Role =", this.role);

}

  checkLoginPage(url: string) {
    this.isLoginPage = url.includes('/login');
  }

  searchTask(event: Event) {
  const value = (event.target as HTMLInputElement).value;
  this.ser.updateSearch(value);
}



  }

