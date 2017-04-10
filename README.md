# SearchApp
People Search App using asp.net MVC, Angular /Web API/Entity Framework Code First
> The Search application has the following functions.
>  * list all the people in the database.
>  * search people by first and last name matching the characters in search text box
>  * Add a new person

### Required tools and software
    - MS visual studio 2015 
    - .Net framework 4.5.2
    - Node.js for running Angular unit tests 

> ### Running the application
>   + Clone the repository to your PC
>   + Build the solution (this will download the required nuget packages)
>   + Set the **SearchWebApp** as your startup project
>   + Run the Application from VS

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

