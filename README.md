# Blood Bank API

## Overview
The Blood Bank API is a RESTful service designed to manage blood donations and their status. It allows users to perform CRUD operations, pagination, and search functionalities on blood bank records. The API is built using C# and ASP.NET Core.

## Postman
[<img src="https://run.pstmn.io/button.svg" alt="Run In Postman" style="width: 128px; height: 32px;">](https://god.gw.postman.com/run-collection/25790862-9dfd5462-ae8b-492a-9497-27698a3b748f?action=collection%2Ffork&source=rip_markdown&collection-url=entityId%3D25790862-9dfd5462-ae8b-492a-9497-27698a3b748f%26entityType%3Dcollection%26workspaceId%3D0d022c77-fde7-44f4-b409-57e85ddf2d8c)

## Features
- **CRUD Operations**: Add, retrieve, update, and delete blood bank entries.
- **Pagination**: Retrieve blood bank entries in paginated format.
- **Search Functionality**: Search blood bank entries by donor name, blood type, or status.
- **Blood Type Regex Validation**:
     - Implementation:
       ``` C#
       private static Regex BloodTypeRegex = new Regex(@"^(A|B|AB|O)[+-]$");

       if (!BloodTypeRegex.IsMatch(newBloodEntry.BloodType)){
             return BadRequest("Invalid blood type. Valid types are A+, A-, B+, B-, AB+, AB-, O+, O-.");
       }
       ```

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
   - `GET /api/BloodBank`
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
   - `GET /api/bloodbank/page?page={pageNumber}&size={pageSize}`
   - Example:
     - `GET /api/bloodbank/page?page=1&size=5`
    
   ![image](https://github.com/user-attachments/assets/67f06d9c-7f66-4420-b912-955677bfe427)

   ---

   ![image](https://github.com/user-attachments/assets/a3dffe25-277b-467b-81a8-fb276df19f90)



### Search Functionality

1. **Search blood entries by blood type**
   - `GET /api/bloodbank/searchByBlood?bloodType={bloodType}`
   - Example:
     - `GET /api/bloodbank/searchByBlood?bloodType=O-`

   ![image](https://github.com/user-attachments/assets/9a8d2c08-a830-4b23-ad51-f9ffc9646a12)


   ---

   ![image](https://github.com/user-attachments/assets/108b2d28-3ec5-4f2f-9fc2-738e8b01cf22)



3. **Search blood entries by status**
   - `GET /api/bloodbank/searchByStatus?status={status}`
   - Example:
     - `GET /api/bloodbank/searchByStatus?status=Expired`

   ![image](https://github.com/user-attachments/assets/508d74fc-fc89-40e2-b56c-aac4578cc303)

   ---

   ![image](https://github.com/user-attachments/assets/f322f97d-2cfe-4b61-b9e2-dfa917ecf57f)



5. **Search blood entries by donor name**
   - `GET /api/bloodbank/searchByDonorName?donorName={donorName}`
   - Example:
     - `GET /api/bloodbank/searchByDonorName?donorName=George`

   ![image](https://github.com/user-attachments/assets/d4b12189-61b9-4c57-9adc-c97f8091f3d4)


   ---

   ![image](https://github.com/user-attachments/assets/c6d9a33b-970c-4d6b-9162-dd7314cab60b)



### Sort Functionality

1. **Sort By Blood Type**
   - `GET /api/BloodBank/sort`
   - Example:
     - `GET /api/BloodBank/sort?by=bloodtype&order=des`
   - params: Ascending By Default, Can be given descending value as well...
   - ..........................................................  Ascending  ..........................................................

   ![image](https://github.com/user-attachments/assets/9819bf11-b318-4f64-98f6-464092d91142)

   ---

   - ..........................................................  Descending  ..........................................................


   ![image](https://github.com/user-attachments/assets/81c3988a-b6cd-4c72-b9b9-bc6b6f6fdacf)




2. **Sort By Collection Date**
   - `GET /api/BloodBank/sort`
   - Example:
     - `GET /api/BloodBank/sort?by=CollectionDate&order=desc`
   - ..........................................................  Descending  ..........................................................

     ![image](https://github.com/user-attachments/assets/309aa05e-af5b-45e5-b94f-5fcf9d005ffa)

   - ..........................................................  Ascending  ..........................................................

     ![image](https://github.com/user-attachments/assets/e0113e45-cd68-4d77-a7e9-cc307fe8b05c)


### Filter Functionality

- `GET /api/BloodBank/filter`
- Example:
   - `GET /api/BloodBank/filter?bloodType=A-&donorName=Jack&status=Expired`
- Params: Takes multiple optional parameters like BloodType, Status, Donor Name,etc. Filters the entries based on all these parameters.

  ![image](https://github.com/user-attachments/assets/21924f16-5ec5-4336-9f16-2a79347f36f5)

  ---

  ![image](https://github.com/user-attachments/assets/d524d31c-e657-4c14-bab6-dea6f2d32a8f)



## Setup Instructions

### Prerequisites
- .NET 6 or later
- Visual Studio or Visual Studio Code
- Postman for testing API endpoints


