# NegativeInfoService [[not-production ready]]

This is a conceptual service for credit negation and integration with credit bureaus made up in ASP.NET Core 3.1.

## Features

* List active negations
* Retrieve a specific negation
* Create a negation
* Resolve a negation

## Architecture

* **Application:** Module with services (high level business logic).
* **Domain:** Module with core business classes
* **Domain.UnitTest:** Module with unit tests to domain classes
* **Infra:** Module with infrastructure classes such as AWS integration.
* **Infra.Data:** Module with specific database classes
* **Infra.IoC:** Module that implement inversion of control
* **Web.API:** Module that implement application interface (web framework)

## Getting Started

	$ dotnet build
	$ dotnet run --project ./NegativeInfoService.Web.API/NegativeInfoService.Web.API.csproj

Open the browser and enter:

* [GET] https://localhost:5001/api/negation/index to show all action negations
* [GET] https://localhost:5001/api/negation/show/{id} to show specific negation
* [POST] https://localhost:5001/api/negation/create to create a negation
* [DELETE] https://localhost:5001/api/negation/delete to resolve a negation

## Unit test

	$ dotnet test