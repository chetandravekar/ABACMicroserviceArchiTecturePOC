@echo Launch CustomerService

title CustomerService
dotnet build --configuration "Release"
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --project C:\Projects\Install\abac-microservice-poc\CustomerMicroservice\CustomersAPIServices.csproj