import { Component, OnInit } from '@angular/core';
import { AngularcoreserviceService } from '../angularcoreservice.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {

  dashboardData: any = {};
  userName: string = '';
  role: string = '';

  constructor(
    private ser: AngularcoreserviceService
  ) {}

  ngOnInit(): void {

    this.userName =
      localStorage.getItem('fullName') || '';

    this.role =
      localStorage.getItem('role') || '';

    console.log(
      'Role From LocalStorage =',
      this.role
    );

    this.ser.getDashboardData(this.role)
      .subscribe({

        next: (res: any) => {

          console.log(
            'Dashboard API Response =',
            res
          );

          this.dashboardData = res;
        },

        error: (err: any) => {

          console.log(
            'Dashboard Error =',
            err
          );
        }
      });
  }

  isAdmin(): boolean {
    return this.role === 'Admin';
  }

  isDeveloper(): boolean {
    return this.role === 'Developer';
  }

  getTotalPriorityTasks(): number {

    return (
      (this.dashboardData.completedTasks || 0) +
      (this.dashboardData.pendingTasks || 0) +
      (this.dashboardData.inProgressTasks || 0)
    );
  }

  getHighPercentage(): number {

    const total =
      this.getTotalPriorityTasks();

    if (total === 0) return 0;

    return Math.floor(
      (this.dashboardData.completedTasks / total)
      * 100
    );
  }

  getMediumPercentage(): number {

    const total =
      this.getTotalPriorityTasks();

    if (total === 0) return 0;

    return Math.floor(
      (this.dashboardData.pendingTasks / total)
      * 100
    );
  }

  getLowPercentage(): number {

    return (
      100 -
      this.getHighPercentage() -
      this.getMediumPercentage()
    );
  }
}