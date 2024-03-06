
# WEB API Number ordering .NET

API for number ordering.

* Project implemented by using "Repository pattern" architecture.
* Implemented "Swagger" documentation. Each controller and DTO is documented.
* Business layer covered with Unit Tests.

## Development environment

* To be able to build and run this project .NET8.0 is required.

## Installation guide

To trust the certificate run `dotnet dev-certs https --trust` (Windows and macOS only).

To build/run this project go to ../API location and execute
`dotnet run` command. For more information click [here](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run).

## Usage

To use Swagger after running this project go to:
https://localhost:7017/swagger.

## Testing

To build/run this project go to ../UnitTests location and execute
`dotnet test` command.

To get code coverage execute
`dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info` command.