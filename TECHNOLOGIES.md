# Technologies overview

This doc explain the best practices, frameworks & libraries used in this project.

## Backend

### .NET

- .NET 8 - latest version of .NET
- Async Actions
  - All actions that perform I/O are async
  - Read more in [MS Best Practices page](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-8.0#avoid-blocking-calls)
  - This [SO answer](https://stackoverflow.com/a/47760462/12072012) also explains why async actions are important
- Global error handling using IExceptionHandler
  - Introduced in .NET 8
  - I could not find any official documentations for this, but this is the newest way to handle errors globally. Previously, a middleware was used to handle errors globally.
  - These error handling follows the [Problem Details for HTTP APIs](https://tools.ietf.org/html/rfc7807) standard
    - .NET introduced a class called [`ProblemDetails`](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-8.0#problem-details) to make it easier to follow this standard

### EF Core

- An open-source ORM maintained by Microsoft
- Code first approach
  - Easy source control
  - It also feels like divine to create the database using C#
- Fluent API - More control over the database schema

### Authentication

- With .NET 8, Microsoft introduced **API endpoints in the `Identity Framework`**
  - Previously it only supported MVC with Razor pages
  - With this new feature, it is possible to create API endpoints for authentication in a RESTful way
  - **BUT** it is not possible to customize the given endpoints
    - It is only possible to use the default endpoints provided by the framework
    - You cannot add new endpoints or remove existing endpoints
    - You have to use all the endpoints provided by the framework or none of them
    - You can add your own endpoints and use managers provided by the framework to perform authentication
    - But those managers are not designed to be used in a RESTful way
    - So you will end up customizing the managers to make them RESTful
    - So it is better to **do the authentication from scratch**
    - Official documentation for this feature can be found [here](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0)
    - [This SO answer](https://stackoverflow.com/a/77624113/12072012) can be a motivation to skip this feature and do the authentication from scratch
- **JWT** - JSON Web Token
  - Use JWT in header
