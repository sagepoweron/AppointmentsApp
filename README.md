# AppointmentsApp

- This app allows you to store clients, doctors, and appointments in a database. You can perform actions such as Add, Edit, Delete on entities stored in a database.
- The solution contains an API project, Data project, MVC project, and a Test project.
- The Data project contains the code needed to access the database and shared code, such as the name validation.
- The API project uses a Swagger page to access the database.
- In the MVC project there are pages to view clients, doctors, and appointments. There is a search filter to find appointments by client name or doctor name.
- The Test project contains the unit tests for the solution.

Features
1. Query your database using a raw SQL query, not EF
2. Create 3 or more unit tests for your application
3. Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program
4. Make a generic class and use it
5. Make your application an API
6. Have 2 or more tables (entities) in your application that are related and have a function return data from both entities. In entity framework, this is equivalent to a join
7. Make your application asynchronous
8. Implement a regular expression (regex) to ensure a field either a phone number or an email address is always stored and displayed in the same format

- The search filters for the Clients page and Doctors page use a method containing a raw SQL query.
- Has unit tests for a mock client repository and validating valid and invalid lists of names.
- Uses lists of clients, doctors, and appointments.
- Client and Doctor both inherit from the Person class. IClientRepository and IDoctorRepository inherit from IRepository.
- There is an API project alongside the MVC project.
- The Appointments page returns data from the Clients table and Doctors table.
- The client and doctor repositories use asynchronous methods to get a list of items from the database.
- Contains a validation method that checks if a name matches a regex pattern.

Setup
- If the test databases are missing, run add-migration and update-database from the console for the API and MVC projects. They both use separate databases.
- First add some clients, doctors, and then appointments in order to use the search filters in the MVC project.
