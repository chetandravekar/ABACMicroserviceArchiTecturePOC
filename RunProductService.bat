@echo Launch ProductService

title ProductService
dotnet build --configuration "Release"
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --project C:\Projects\Install\abac-microservice-poc\ProductMicroservice\ProductMicroservice.csproj