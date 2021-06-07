# Authenticated Cat Image API
A web API with ASP.NET Core 3.1. 
* Fetchs a random cat image from "https://cataas.com/" and flips the image upside down.  
* Has JWT authentication endpoints.

# Build & Run

* With Visual Studio

    Open the solution file <code>src/CAAS.sln</code> and build/run.

* With .net Command

    Run the following commands at the directory `src/CAAS` :

    `dotnet build`

    `dotnet run`

# Swagger UI
Basic information about the API endpoints can be found at the Swagger UI.
https://localhost:5001/swagger/

(with IIS Express: https://localhost:44372/swagger )

![image](https://user-images.githubusercontent.com/19646608/121062434-5d232080-c7c5-11eb-9b55-7721d19f006d.png)


# Endpoints
* GET  
​/cat​/random
* POST  
/user/register
* GET  
/user/{id}
* POST  
/user/login
# Credits
* Thanks for the cute cats 
https://cataas.com
* Thanks for the detailed user management implementation https://github.com/cornflourblue/aspnet-core-3-registration-login-api

# Contact
volkancicek@outlook.com
 
