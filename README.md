# EMS_oop

## Overview
<br>
The Employee Management System (EMS) is a robust application built using .NET MAUI and Blazor, designed to streamline HR processes by providing 
comprehensive functionalities for managing employees, payrolls, time records, and generating various reports. <br>
The system integrates with a MySQL database to ensure efficient data storage and retrieval.

<br>

## Functionality

### Employee Management:
•	Add, Update, Delete: Manage employee records with CRUD operations. <br>
•	Search and Filter: Easily search for employees by various criteria. <br>
•	Role and Status Management: View and update employee roles and statuses.<br>

### Payroll Management:
•	Payroll Details: Manage payroll information including base salary, overtime pay, deductions, and net salary. <br>
•	Payroll Calculations: Automatically calculate payroll based on time records and predefined rules.<br>

### Time Records:
•	Time Tracking: Record and manage employee working hours.<br>
•	Attendance Management: Track attendance and generate time-based reports.<br>

### Reporting:
•	Report Generation: Generate detailed reports on employees, payrolls, and time records.<br>
•	Export to Excel: Export data to Excel files for further analysis and record-keeping.<br>

### Database Integration:
•	MySQL Database: Uses MySQL for efficient data storage and retrieval.<br>
•	Data Consistency: Ensures data consistency and integrity across the application.<br>


## Problem Solved

The EMS addresses several key challenges in HR management:<br>
•	Data Management: Provides a centralized system to manage employee data, reducing the need for manual record-keeping.<br>
•	Efficiency: Automates payroll calculations and time tracking, saving time and reducing errors.<br>
•	Reporting: Offers comprehensive reporting capabilities, enabling quick access to important HR metrics and insights.<br>
•	Integration: Ensures seamless integration with a MySQL database, providing reliable data storage and retrieval.<br>

## Key Files
•	MauiProgram.cs: Configures the .NET MAUI application.<br>
•	Employee.cs: Defines the Employee model.<br>
•	EmployeeService.cs: Provides services for managing employee data. <br>
•	DBService.cs: Handles database operations. <br>
•	Report.cs: Defines the Report model. <br>
•	ReportService.cs: Provides services for generating reports. <br>
•	Payroll.cs: Defines the Payroll model. <br>
•	PayrollService.cs: Manages payroll-related operations. <br>
•	TimeRecordService.cs: Manages time records and attendance. <br>
•	Home.razor: The main Blazor component for the home page.

