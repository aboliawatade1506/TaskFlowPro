import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable ,BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AngularcoreserviceService {

  private taskUrl = 'http://localhost:5031/api/Task';
  private loginUrl = 'http://localhost:5031/api/User/login';
  private dashboardurl='http://localhost:5031/api/Dashboard';
  private profileurl='http://localhost:5031/api/Profile';

   // Common Base URL
  // private baseUrl = 'http://localhost:5031/api';

   // API URLs
  // private taskUrl = `${this.baseUrl}/Task`;
  // private loginUrl = `${this.baseUrl}/User/login`;
  // private dashboardUrl = `${this.baseUrl}/Dashboard`;
  // private profileUrl = `${this.baseUrl}/Profile`;


   private searchSource = new BehaviorSubject<string>('');
  searchText$ = this.searchSource.asObservable();

  constructor(private http: HttpClient) { }

  // for fetch all task
  getTask(role: string): Observable<any[]> {
  return this.http.get<any[]>(`${this.taskUrl}/role/${role}`);
}

  // for add task
  addTask(data: any) {
  return this.http.post(this.taskUrl, data);
}

// for update task
updateTask(task: any) {
  console.log("TASK IN SERVICE:", task);
  console.log("TASK ID:", task.TaskId);
  return this.http.put(`${this.taskUrl}/${task.TaskId}`, task);
}

// for delete task
 deleteTask(id: string) {
    return this.http.delete(`${this.taskUrl}/${id}`);
  }

  // for login user
  login(data: any): Observable<any> {
    return this.http.post<any>(this.loginUrl, data);
  }

  // for dashboard fetch data
  getDashboardData(role: string){
  return this.http.get(`${this.dashboardurl}/${role}`);
}

 // Get Profile
   getProfile(email: string): Observable<any> {
    return this.http.get(`${this.profileurl}/GetProfile/${email}`);
  }

  // Update Profile
  updateProfile(profileData: any): Observable<any> {
    return this.http.put(`${this.profileurl}/UpdateProfile`, profileData);
  }

  // for search data on textbox
   updateSearch(text: string) {
    this.searchSource.next(text);
  }
}