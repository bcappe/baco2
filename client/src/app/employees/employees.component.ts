import { Component, OnInit } from '@angular/core';
import { IEmployee } from '../shared/models/employee';
import { EmployeesService } from './employees.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit {
  employees: IEmployee[];

  constructor(private employeesService: EmployeesService) { }

  ngOnInit(): void {
    this.employeesService.getEmployees().subscribe({
      next: (response) => this.employees = response.data,
      error:(e) =>console.error(e)   
    });

  }}
