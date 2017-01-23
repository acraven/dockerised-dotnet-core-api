#!/bin/bash
set -e

dotnet restore
dotnet publish DotnetApiReference.Api --configuration Release --output binaries
dotnet test DotnetApiReference.Api.Tests --configuration Release
