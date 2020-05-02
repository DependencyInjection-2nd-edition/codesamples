# Sample code for Dependency Injection: Principles, Practices, and Patterns

This repository contains sample code for the book [Dependency Injection Principles, Practices, and Patterns](https://manning.com/seemann2). You can find the code shown in the book in this repository, embedded in functioning sample applications.

## Required components

All solutions are built using Visual Studio 2017 on .NET Core, although you should be able to use the solutions with Visual Studio 2019 and Visual Studio Code on Mac and Linux as well.

Some of the solutions themselves use various open source DI Containers to compile and run:
- Autofac: https://autofac.org
- Simple Injector: https://simpleinjector.org
- Microsoft.Extensions.DependencyInjection: https://github.com/aspnet/DependencyInjection

## Database setup

In contrast to the book's code listings, the sample applications make use of SQLite, because they allow the application to run without having to setup SQL Server database locally. The SQLite databases are included in this download. You should be able to run the sample applications as-is.

If you wish to use SQL Server instead, the applications require that their databases are set up correctly. Each application requiring a database have associated T-SQL scripts.

After setting up the database, you need to add the connection string to the `applicationsettings.json` files of the applications, and change the CommerceContext to use SQL Server instead of SQLite.

## Included code samples

The following code samples are included in this repository:

* **HelloDI**: The "Hello DI" example of section 1.2 giving a first example of DI.
* **ElectricalAppliances**: In section 1.1.2 "Understanding the purpose of DI," electrical wiring is compared to software design patterns, building analogies for Null Object, Decorator, Composite, and Adapter design patterns. This is done without showing any code. The ElectricalAppliances solution shows how this would map to code.
* **MarysECommerce**: The "Mary's E-Commerce" example that runs through chapter 2, which demonstrates a tightly coupled web application that doesn't apply DI.
* **RightECommerce**: The "Right E-Commerce" example that runs through chapter 3, which demonstrates how to create a loosely coupled code base using DI. This solution also includes:
  * The "Currency Convertions" example of section 4.2.4, demonstrating **Constructor Injection**.
  * The "Update Currency" example of section 7.1.1, demonstrating how to compose console applications.
  * The "Currency Monitoring" example of section 8.3.3, demonstrating both **Scoped Lifestyle** using a console application.
* **UWPProductManagementClient**: The "Product-management rich client" example of section 7.2, demonstrating how to compose UWP applications. Note that WPF composition is very similar to UWP. This solution includes:
  * The "Circuit Breaker" example of section 9.2.1.
* **Greeter**: The "Greeter" example of section 9.1.1, demonstrating the Decorator design pattern.
* **AopECommerce**: The "SOLID as driver for AOP" example of chapter 10, which demonstrates how SOLID design can be applied to achieve Aspect-Oriented Programming. This solution also includes:
  * The "Refactoring from Constructor Over-Injection to domain events" example of section 6.1.3.
  * An example of wiring the AOP example using Pure DI (the Commerce.Web.PureDI project).
  * An example of wiring the AOP example using Pure DI (the Commerce.Web.PureDI project).
  * An example of wiring the AOP example using Autofac as discussed in chapter 13 (see the Commerce.Web.Autofac project).
  * An example of wiring the AOP example using Simple Injector as discussed in chapter 14 (the Commerce.Web.SimpleInjector project).
  * An example of wiring the AOP example using Microsoft.Extensions.DependencyInjection as discussed in chapter 14 (the Commerce.Web.MS.DI project).

## Contributions

All code is released as open source, and we do take pull requests. We've left blank some implementation code not central to the topic of dependency injection, or at least in a state that could be improved.

There's also fewer unit tests than we'd like.

You can help improve the code, but with one important limitation:

**The code that appears in the book musn't be changed.**

While an open source code base is a dynamic entity, the printed book can't be changed, and even the eBooks are unlikely to see drastic changes. We'll do our best to mark the parts of the code that mustn't be changed.

For the rest of the code base, we take pull requests, but please follow [these tips for good pull requests](https://blog.ploeh.dk/2015/01/15/10-tips-for-better-pull-requests).

(Last updated 2020.05.02 with the list of included examples)
