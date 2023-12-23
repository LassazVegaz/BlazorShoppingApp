# Technologies overview

This doc explain the best practices, frameworks & libraries used in this project.

## Backend

### .NET

- .NET 8 - latest version of .NET
- Async Actions
  - All actions that perform I/O are async
  - Read more in [MS Best Practices page](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-8.0#avoid-blocking-calls)
  - This [SO answer](https://stackoverflow.com/a/47760462/12072012) also explains why async actions are important

### EF Core

- An open-source ORM maintained by Microsoft
- Code first approach
  - Easy source control
  - It also feels like divine to create the database using C#
- Fluent API - More control over the database schema
