@echo Launch IdentityService

title IdentityService
dotnet build --configuration "Release"
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --project C:\Projects\Install\abac-microservice-poc\IdentityService\IdentityService.csproj