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

Open the browser and enter https://localhost:5001

## Unit test

	$ dotnet test