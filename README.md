# Library Management

Library Management System API allows librarians to manage books, patrons, and borrowing records.

## Overview
This API, built using .NET, manages a library system and provides endpoints for various operations, including managing accounts, books, borrowing records, patrons, and users.

## Running the Application

To run the application, follow these steps:
1. Clone the repository from GitHub: git clone [https://github.com/your/repository.git](https://github.com/hema325/LibraryManagementTask.git)
2. Navigate to the project directory: cd project_directory
3. Build the solution: dotnet build
4. Run the application: dotnet run

## Authentication and Authorization
Authentication is implemented using JWT (JSON Web Tokens). To obtain an access token, send a POST request to the /api/account endpoint with your credentials. Include the access token in the Authorization header of subsequent requests.

## Endpoints
For detailed information about each endpoint, including parameters, request bodies, responses, and authentication requirements, please refer to the Swagger documentation.

![Screenshot 2024-04-21 185338](https://github.com/hema325/LibraryManagementTask/assets/74411228/3bf5355f-894d-4021-b6ad-e52b39f659c7)

![Screenshot 2024-04-21 185349](https://github.com/hema325/LibraryManagementTask/assets/74411228/f5a51f38-fdd5-4d43-8566-a37db62c4fb1)

## Error Handling
The API utilizes a global exception handler to centrally handle errors. This ensures consistent error handling logic across all endpoints. Error responses will include a descriptive message.

![Screenshot 2024-04-21 185754](https://github.com/hema325/LibraryManagementTask/assets/74411228/a3b0117e-7ea3-431e-afed-22f41fea03e9)

