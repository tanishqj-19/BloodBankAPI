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
   - `POST /api/BloodBank`
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
     ![image](https://github.com/user-attachments/assets/4c4da226-cfe7-42e5-9c65-db3a95646914)
     
     ---
     
     ![image](https://github.com/user-attachments/assets/da9178e9-2319-4163-a5d4-4ebe353e11e1)



2. **Get all blood bank entries**
   - `GET https://localhost:7065/api/BloodBank`
     ![image](https://github.com/user-attachments/assets/ae27f396-aec4-472a-9fc6-69b696be1462)

      ---
     
     ![image](https://github.com/user-attachments/assets/66889fad-7141-4819-b8e0-4f548f9a0683)


3. **Get a specific blood entry by ID**
   - `GET /api/BloodBank/{id}`
      ![image](https://github.com/user-attachments/assets/469cd2f0-628c-4eab-a201-d7477fc5513e)

     ---

     ![image](https://github.com/user-attachments/assets/d5ec9529-d9cb-4cd2-aa80-061ae87222eb)



4. **Update an existing blood entry**
   - `PUT /api/BloodBank/`
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
  
     
     ![image](https://github.com/user-attachments/assets/a1134816-48b2-4f5f-b3c5-f4e8402a545e)

     ---

     ![image](https://github.com/user-attachments/assets/f1acc441-1e05-4fc5-a3fe-4afa6206ac1b)

   
     
 

6. **Delete a blood entry**
   - `DELETE /api/BloodBank/{id}`

     ![image](https://github.com/user-attachments/assets/fe14e2b7-0b55-4fe2-b208-7923d9351599)

     ---

     ![image](https://github.com/user-attachments/assets/d06551d1-232a-48e4-b654-02a02af4e9b2)



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
