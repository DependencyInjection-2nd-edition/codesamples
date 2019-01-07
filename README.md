# Sample code for Dependency Injection: Principles, Practices, Patterns

This repository contains sample code for the book [Dependency Injection: Principles, Practices, Patterns](https://www.manning.com/books/dependency-injection-principles-practices-patterns). You can find the code shown in the book in this repository, embedded in functioning sample applications.

## Required components

All solutions are built using Visual Studio 2017 on .NET Core, although you should be able to use the solutions with Visual Studio Code on Mac and Linux as well.

Some of the solutions themselves use various open source DI Containers to compile and run:
- Autofac: https://autofac.org
- Simple Injector: https://simpleinjector.org
- Microsoft.Extensions.DependencyInjection: https://github.com/aspnet/DependencyInjection

## Database setup

In contrast to the book's code listings, the sample applications make use of SQLite, because they allow the application to run without having to setup SQL Server database locally. The SQLite databases are included in this download. You should be able to run the sample applications as-is.

If you wish to use SQL Server instead, the applications require that their databases are set up correctly. Each application requiring a database have associated T-SQL scripts.

After setting up the database, you need to add the connection string to the `applicationsettings.json` files of the applications, and change the CommerceContext to use SQL Server instead of SQLite.

## Contributions

All code is is released as open source, and we do take pull requests. We've left blank some implementation code not central to the topic of dependency injection, or at least in a state that could be improved.

There's also fewer unit tests than we'd like.

You can help improve the code, but with one important limitation:

**The code that appears in the book musn't be changed.**

While an open source code base is a dynamic entity, the printed book can't be changed, and even the eBooks are unlikely to see drastic changes. We'll do our best to mark the parts of the code that mustn't be changed.

For the rest of the code base, we take pull requests, but please follow [these tips for good pull requests](http://blog.ploeh.dk/2015/01/15/10-tips-for-better-pull-requests).

(Last updated 2019.01.07 while finalizing the manuscript)