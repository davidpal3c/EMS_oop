# EMS_oop

## Overview
<br>
The Employee Management System (EMS) is a robust application built using .NET MAUI and Blazor, designed to streamline HR processes by providing 
comprehensive functionalities for managing employees, payrolls, time records, and generating various reports. 
The system integrates with a MySQL database to ensure efficient data storage and retrieval.

<br>

## Functionality

### Employee Management:
•	Add, Update, Delete: Manage employee records with CRUD operations.
•	Search and Filter: Easily search for employees by various criteria.
•	Role and Status Management: View and update employee roles and statuses.

### Payroll Management:
•	Payroll Details: Manage payroll information including base salary, overtime pay, deductions, and net salary.
•	Payroll Calculations: Automatically calculate payroll based on time records and predefined rules.

### Time Records:
•	Time Tracking: Record and manage employee working hours.
•	Attendance Management: Track attendance and generate time-based reports.

### Reporting:
•	Report Generation: Generate detailed reports on employees, payrolls, and time records.
•	Export to Excel: Export data to Excel files for further analysis and record-keeping.

### Database Integration:
•	MySQL Database: Uses MySQL for efficient data storage and retrieval.
•	Data Consistency: Ensures data consistency and integrity across the application.


## Problem Solved

The EMS addresses several key challenges in HR management:
•	Data Management: Provides a centralized system to manage employee data, reducing the need for manual record-keeping.
•	Efficiency: Automates payroll calculations and time tracking, saving time and reducing errors.
•	Reporting: Offers comprehensive reporting capabilities, enabling quick access to important HR metrics and insights.
•	Integration: Ensures seamless integration with a MySQL database, providing reliable data storage and retrieval.

## Key Files
•	MauiProgram.cs: Configures the .NET MAUI application.
•	Employee.cs: Defines the Employee model.
•	EmployeeService.cs: Provides services for managing employee data.
•	DBService.cs: Handles database operations.
•	Report.cs: Defines the Report model.
•	ReportService.cs: Provides services for generating reports.
•	Payroll.cs: Defines the Payroll model.
•	PayrollService.cs: Manages payroll-related operations.
•	TimeRecordService.cs: Manages time records and attendance.
•	Home.razor: The main Blazor component for the home page.

