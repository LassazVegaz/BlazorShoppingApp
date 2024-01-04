# Authentication Library for Trending App

This library is used for authentication in Trending App microservices.

This library has limited customizations intentionally.
This library enforce some rules to make sure that all microservices are using the same authentication rules.

## How to use this library

### Validate JWT token

Add the authentication extension provided by this library to your service:

```cs
builder.Services.AddTrendingAppAuthentication();
```

Every micrervice should add this.

### Generate JWT token

This library provides a service to generate JWT tokens. Add the service by calling extension method `AddTokensService`.
This service can be consumed by using interface `ITokensService`.
Which ever microservice that needs to generate JWT tokens should use this service.
Other microservices don't need to use this service.

## Some information about this library that can be useful for public:

- Authentication can be done using Authorization header or cookie.
	- But both of them use JWT tokens.
	- If authentication is done using cookie, the same JWT in cookie should be in Authorization header to avoid CSRF attacks.
	- Authorization Header use Bearer token format: `Bearer <JWT token>`.
	- Cookie just include JWT token in it.
	- Cookie's name is `TrendingApp-Auth`.
- JWT signing keys
	- Symmetric key is used for signing JWT tokens.
	- Symmetric key is hardcoded for now.
- JWT payload includes
	- `userId` provided when generating the token.