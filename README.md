# Integration tests in ASP.NET Core 3.0 and EF Core


The reason for this repo is to show the issue with the integration test on the latest .NET Core 3.0 together with EF Core.

The Application is using the SQLite DB provider and the integration test should use the InMemory DB provider from EF Core. The integration test is following Microsoft documentation "[Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.0)" which shows the approach of how to use a [customize the WebApplicationFactory](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.0#customize-webapplicationfactory).
