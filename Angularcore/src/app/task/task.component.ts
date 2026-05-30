import { Component, TemplateRef, ViewChild, AfterViewInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule,MatTableDataSource  } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { AngularcoreserviceService } from '../angularcoreservice.service';
import { MatTooltipModule } from '@angular/material/tooltip';
import Swal from 'sweetalert2';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';

@Component({
  selector: 'app-task',
  standalone: true,
  imports: [
     CommonModule,
  MatFormFieldModule,
  FormsModule,
  MatInputModule,
  MatSelectModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatTabsModule,
  MatButtonModule,
  MatIconModule,
  MatTableModule,
  MatCardModule,
  MatDialogModule,
  MatTooltipModule
  ],
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent implements AfterViewInit {

   displayedColumns: string[] = ['taskName', 'priority', 'assignBy','dueDate', 'action' ];

taskData = new MatTableDataSource<any>();
  // add task
  newTask:any = {};
//view task
  selectedTask: any = {};
  //edit task
  isEditMode = false;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private ser: AngularcoreserviceService,
     private dialog: MatDialog)
   {
    this.loaddata();

    //filter task
    this.ser.searchText$.subscribe((text) => {
      this.taskData.filter = text.trim().toLowerCase();
    });

    this.taskData.filterPredicate = (data: any, filter: string) => {
      return (
        data.taskName?.toLowerCase().includes(filter) ||
        data.taskStatus?.toLowerCase().includes(filter) ||
        data.assignBy?.toLowerCase().includes(filter) ||
        data.priority?.toLowerCase().includes(filter) ||
        new Date(data.dueDate).toDateString().toLowerCase().includes(filter)
      );
    };
  }
    
  ngAfterViewInit() {
  this.taskData.paginator = this.paginator;
}

  //load task
 loaddata() {

  const role = localStorage.getItem('role');

  this.ser.getTask(role!)
    .subscribe({
      next: (res: any[]) => {

        console.log("TASKS =", res);

        this.taskData.data = res;

      },
      error: (err) => {
        console.log(err);
      }
    });
}

//open modal
  openTaskModal(template: TemplateRef<any>) {
    this.isEditMode = false;
    this.newTask = {};

    this.dialog.open(template, {
      width: '900px',
      maxWidth: '95vw'
    });
  }

//close modal
closeModal() {
  this.dialog.closeAll();
}

//save task
saveTask() {

  if (this.isEditMode) {

    this.ser.updateTask(this.newTask).subscribe({
      next: () => {
        this.loaddata();
        this.closeModal();
        this.newTask = {};
        this.isEditMode = false;

        Swal.fire({
          title: 'Updated!',
          text: 'Task updated successfully.',
          icon: 'success',
          timer: 1500,
          showConfirmButton: false
        });
      },
      error: (err) => {
        console.log(err);

        Swal.fire({
          title: 'Error!',
          text: 'Task could not be updated.',
          icon: 'error'
        });
      }
    });

  } else {

    this.ser.addTask(this.newTask).subscribe({
      next: () => {
        this.loaddata();
        this.closeModal();
        this.newTask = {};

        Swal.fire({
          title: 'Saved!',
          text: 'Task added successfully.',
          icon: 'success',
          timer: 1500,
          showConfirmButton: false
        });
      },
      error: (err) => {
        console.log(err);

        Swal.fire({
          title: 'Error!',
          text: 'Task could not be saved.',
          icon: 'error'
        });
      }
    });
  }
}

//view task
viewTask(template: TemplateRef<any>, task: any) {
  this.selectedTask = task;

  this.dialog.open(template, {
    width: '900px'
  });
}

//edit task
 editTask(task: any, template: TemplateRef<any>) {
  this.closeModal();
  this.isEditMode = true;

  this.newTask = {
    TaskId: task.taskId || task.id,
    taskName: task.taskName,
    taskDescription: task.taskDescription,
    priority: task.priority,
    taskStatus: task.taskStatus,
    assignBy: task.assignBy,
    dueDate: task.dueDate
  };

  console.log("EDIT TASK:", this.newTask);

  this.dialog.open(template, {
    width: '900px'
  });
}

// delete task
deleteTask(task: any) {
  Swal.fire({
    title: 'Are you sure?',
    text: 'This task will be deleted permanently!',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#d33',
    cancelButtonColor: '#3085d6',
    confirmButtonText: 'Yes, delete it!',
    cancelButtonText: 'Cancel'
  }).then((result) => {

    if (result.isConfirmed) {
      this.ser.deleteTask(task.taskId).subscribe({
        next: () => {
          this.loaddata();

          Swal.fire({
            title: 'Deleted!',
            text: 'Task deleted successfully.',
            icon: 'success',
            timer: 1500,
            showConfirmButton: false
          });
        },
        error: (err) => {
          console.log(err);

          Swal.fire({
            title: 'Error!',
            text: 'Task could not be deleted.',
            icon: 'error'
          });
        }
      });
    }

  });
}

  //set colour on priority
  getPriorityClass(priority: string): string {
  switch (priority?.toLowerCase()) {
    case 'high':
      return 'high-priority';
    case 'critical':
      return 'critical-priority';
    case 'medium':
      return 'medium-priority';
    case 'low':
      return 'low-priority';
    default:
      return '';
  }
}
}

