FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /build

COPY ./DotnetCoreApiReference/DotnetCoreApiReference.csproj ./DotnetCoreApiReference/DotnetCoreApiReference.csproj
RUN dotnet restore ./DotnetCoreApiReference/DotnetCoreApiReference.csproj

COPY ./DotnetCoreApiReference.Tests/DotnetCoreApiReference.Tests.csproj ./DotnetCoreApiReference.Tests/DotnetCoreApiReference.Tests.csproj
RUN dotnet restore ./DotnetCoreApiReference.Tests/DotnetCoreApiReference.Tests.csproj

COPY . .

RUN dotnet publish -c Release ./DotnetCoreApiReference/DotnetCoreApiReference.csproj
RUN dotnet test -c Release ./DotnetCoreApiReference.Tests/DotnetCoreApiReference.Tests.csproj

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine
WORKDIR /app

HEALTHCHECK --interval=10s --timeout=3s --start-period=10s --retries=3 \
  CMD wget -O - http://localhost:8080/ping | grep Pong!

EXPOSE 8080

COPY --from=build /build/DotnetCoreApiReference/bin/Release/netcoreapp2.1/publish .

# Using the exec form of CMD allows SIGINT to be passed from container to app
CMD ["dotnet", "DotnetCoreApiReference.dll"]
