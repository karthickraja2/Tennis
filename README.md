# TennisCourtAPI

## This repository contains source code for a RESTFUL API for a Tennis Court Booking Application.

- The api has been implemented using .net core, targeting the latest stable version, i.e. .NET 6
- Followed clearn architecture and ensured separation of concerns when creating the api
- Adopted the DTO design pattern to avoid exposing the entire database entities to a remote server/interface, thus ensuring loose coupling between the model and api.
- Used AutoMapper to avoid manual objects transition from DTO type to Model type/custom mapping between the DTO objects and entities.
- Used Repository Pattern to isolate DataAccess layer and business logic.
- Used generic repository design pattern to avoid code repetitions across different repositories for common actions.
- Used data validations extensively to reduce data entry errors and ensure consistency.
- Used filters at different levels of request processing for concerns such as authorization, caching, etc.
- Used Caching mechanism to enhance and speed up the accessibly of commonly accessed data.
- Followed Single Responsible Principle and Dependency Injection for better manageability and loose coupling.
- Used EF Core to work with the database.
- Used authorization and authentication to ensure secured sharing of information. 
- Used JWT to secure transfer of data, ensuring only logged-in users are able to access the data.

## Functionalities

- register and login with a user account
- list, create, update, patch and delete tennis court information
- allow users to to create, view, update and cancel court bookings 

## Instructions to run and test the API
- Must have installed:
  - Visual Studio 2022
  - .NET 6.0
  - SQL Server Management Studio/SQL Server

- For the security concerns, I have gitignored migrations and appsettings.json file.
- Update the following lines in your appsettings.json file:
    
    - For Db connection

        "ConnectionStrings": {
          "DefaultConnection": "Server=<ServerName>;Database=DbName;Integrated Security=True;TrustServerCertificate=True"
          }
    - For Jwt authentication key

        "ApiSettings": {
            "Secret": "This is my secret key to be used for jwt authentication ,dmfkmgmmvmvmvmvmvmvnbnjjfkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkdj"
          }

- Apply the migrations by opening package manager console and run the following commands
  - add-migration "migration name"
  - update-database.
    
- Once succeedded, open your ssms to see if the tables are updated in the defined database.

## Sequence to follow when testing the api:

- run http action for registring and then log in to verify if you are able to. Ensure to have jwt token generated post successful login
- run http actions for Tennis Court functionalities, it does not require you to be registered or logged-in.
- Run http actions for Bookings, but before that, login and copy the JWT token generated by the api
  - Paste "Bearer<Space><copied token>" in the value field of Authentication after clicking it open (Available on the top right).
  - Post successful authorization, you should be able to test the http actions for the Bookings as well.
  - Note: Give the foreign key values for userid and tenniscourtid if any action requires you to do so.
  - Ensure to give only those values which are available inside your Users and TennisCourts Table to avoid foreign key constraint violations.
 
## Should you have any DOUBTS/QUERIES regarding api testing, do not hesitate to email me: iali75637@gmail.com

