# Blood Bank API

## Overview
The Blood Bank API is a RESTful service designed to manage blood donations and their status. It allows users to perform CRUD operations, pagination, and search functionalities on blood bank records. The API is built using C# and ASP.NET Core.

## Features
- **CRUD Operations**: Add, retrieve, update, and delete blood bank entries.
- **Pagination**: Retrieve blood bank entries in paginated format.
- **Search Functionality**: Search blood bank entries by donor name, blood type, or status.

## Model: `BloodBankEntry`
This model represents a blood donation entry in the system. It contains the following fields:
- `Id`: A unique identifier for the entry.
- `DonorName`: Name of the donor.
- `Age`: Donor's age.
- `BloodType`: Blood type (e.g., A+, O-, B+).
- `ContactInfo`: Contact details (phone number or email).
- `Quantity`: Quantity of blood donated (in milliliters).
- `CollectionDate`: Date when the blood was collected.
- `ExpirationDate`: Expiration date for the blood unit.
- `Status`: Current status of the blood entry (Available, Requested, Expired).

## Endpoints

### CRUD Operations

1. **Create a new blood entry**
   - `POST /api/bloodbank`
   - Request Body:
     ```json
     {
       "donorName": "John Doe",
       "age": 25,
       "bloodType": "O+",
       "contactInfo": "1234567890",
       "quantity": 1.5,
       "collectionDate": "2024-03-15T00:00:00",
       "expirationDate": "2024-09-15T00:00:00",
       "status": "Available"
     }
     ```

2. **Get all blood bank entries**
   - `GET /api/bloodbank`

3. **Get a specific blood entry by ID**
   - `GET /api/bloodbank/{id}`

4. **Update an existing blood entry**
   - `PUT /api/bloodbank/{id}`
   - Request Body:
     ```json
     {
       "donorName": "John Doe",
       "age": 26,
       "bloodType": "O+",
       "contactInfo": "1234567890",
       "quantity": 1.5,
       "collectionDate": "2024-03-15T00:00:00",
       "expirationDate": "2024-09-15T00:00:00",
       "status": "Requested"
     }
     ```

5. **Delete a blood entry**
   - `DELETE /api/bloodbank/{id}`

### Pagination

1. **Get paginated list of blood entries**
   - `GET /api/bloodbank?page={pageNumber}&size={pageSize}`
   - Example:
     - `GET /api/bloodbank?page=1&size=5`

### Search Functionality

1. **Search blood entries by blood type**
   - `GET /api/bloodbank/search?bloodType={bloodType}`
   - Example:
     - `GET /api/bloodbank/search?bloodType=O+`

2. **Search blood entries by status**
   - `GET /api/bloodbank/search?status={status}`
   - Example:
     - `GET /api/bloodbank/search?status=Available`

3. **Search blood entries by donor name**
   - `GET /api/bloodbank/search?donorName={donorName}`
   - Example:
     - `GET /api/bloodbank/search?donorName=John`

## Setup Instructions

### Prerequisites
- .NET 6 or later
- Visual Studio or Visual Studio Code
- Postman for testing API endpoints

### Steps to Run the Project
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/blood-bank-api.git
   cd blood-bank-api
