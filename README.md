# An App full of Features

> [!CAUTION]\
> This project is created to add a value to the author's CV. If the project is duplicated without proper credits, it will not add any value to the author's CV. Therefore, if this application is part of any other application, credits should be added. Check [LICENSE.txt](./LICENSE.txt)

Target of this project is to build an application (or system when speaking very technically) full of trending features using trending libraries, frameworks and coding patterns.

Business domain and the requirements of the application are not very focused as much as the focus on technologies. The main traget is to learn new things by doing them. But still it has to be a logical application. So this is basically a shopping app.

## Technologies used

- frontend:
  - NextJS - the underlying framework
  - MUI - UI components and designing
  - React Query | Tanstack Query - data fetching and caching
  - Tanstack Forms - form handling
- backend:
  - Microservices
  - .NET - some microservices will be built using this
  - NestJS - some microservices will be built using this
  - EF Core - ORM for .NET
  - Prisma - ORM for NestJS
  - RabbitMQ - message broker
  - MassTransit - message broker client
  - Saga - distributed transactions
  - MySQL - database

I will add more technologies as I go. Some of the technologies are not decided yet. They are seperated by `|` in the list.\
Detailed explanation of best practices, frameworks & libraries in this project are discussed [here](./TECHNOLOGIES.md).

## Features

- [ ] Authentication
  - [ ] Login
    - [x] Email and password
    - [ ] OpenID Connect
  - [ ] Register
    - [ ] Using methods from login
  - [x] Profile management
    - [x] Change basic info
    - [x] Change email
    - [x] Change password

More features will be added as I go. Some of them are not completely decided yet such as OpenID Connect. I will try to explain why I chose one over the other later when I have time.

## Extra

I want to use micro frontends but still not sure how to do it. I like to use frameworks such as SOLIDStart and SvelteKit. That is why I want to use micro frontends.

## Contribution

This is just a project to learn stuff. If you want to learn new trending stuff together, let's have some discussions. Talk to me on [Twitter](https://twitter.com/lassi2k) or [LinkedIn](https://www.linkedin.com/in/lasindu-w-abb08413a).
