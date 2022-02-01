@echo Launch APIGateway

title APIGateway
dotnet build --configuration "Debug"
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --project C:\Projects\Install\abac-microservice-poc\Gateway.WebApi\Gateway.WebApi.csproj