# API de Búsqueda de Empleados
This API allows STGenetics clients to manage an animal, whether it is a cow or a bull. It enables creating, editing, deleting, and filtering an animal. Additionally, it allows for purchasing animals.

## Functionalities
* Addition of an animal.
* Editing an animal.
* Deleting an animal.
* Listing animals with or without filtering.
* Purchasing available animals.


### Example of the Animal properties:

```json
{
  "AnimalId": 151,
  "Name": "Martina",
  "Breed": "Best Cow",
  "BirthDate": "2019-08-10T20:01:59",
  "Sex": "Female",
  "Price": 5000.00,
  "Status": true
}
```

## JWT for authenticaation
The API uses JWT for authorization and authentication. The user and password will be provided by the system administrator for proper usage of the API. The Authorization header must be added with the prefix "Bearer" followed by the token for proper API usage. If the token is not provided, the API will respond with an HTTP 401 Unauthorized status code.

## Requirements
.NET 7 SDK instalado.
SQL Server 2019 for database



## How to Use it
 
* Clone the repository or download the Api files.
* Open the project in your IDE of your preferencia( by default is Visual Studio).
* Restore nugets packages if it is necesary.
* Build an compile the API.