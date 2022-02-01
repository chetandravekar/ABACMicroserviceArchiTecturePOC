@echo Launch TestUIApplication

title TestUIApplication
dotnet build --configuration "Release"
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --project C:\Projects\Install\abac-microservice-poc\Test\Test.csproj