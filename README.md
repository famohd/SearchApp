# SearchApp
People Search App using asp.net MVC, Angular /Web API/Entity Framework Code First
> The Search application has the following functions.
>  * list all the people in the database.
>  * search people by first and last name matching the characters in search text box
>  * Add a new person


## Project Structure

### SearchWebApp
* Asp.net mvc web application
   * controllers
* AngularJS
   * Anguar-App
      * src
      * test (node js install required for running Karma/jasmine tests)
* Bootstrap

### SearchWebApp.Tests
- Unit tests for SearchWebApp Controllers
    - using NUnit, Moq


### SearchWebAPI
+ Web API consumed by Searh Web application
    + Controller
    
### SearchWebAPI.Tests
- Unit tests for SearchWebAPI Controller
    - using NUnit, Moq


### Search.Repository
- EF 6x Data Context
- Model
- Repository
- Service

    
### Search.Repository.Tests
- Unit tests for Repository, Search service
    - using NUnit, Moq

