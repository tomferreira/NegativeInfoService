# NegativeInfoService [[not-production ready]]

This is a conceptual service for credit negation and integration with credit bureaus made up in ASP.NET Core 3.1.

## Features

* List active negations
* Retrieve a specific negation
* Create a negation
* Resolve a negation

## Getting Started

	$ dotnet build
	$ dotnet run --project ./NegativeInfoService.Web.API/NegativeInfoService.Web.API.csproj

Open the browser and enter https://localhost:5001

## Unit test

	$ dotnet test