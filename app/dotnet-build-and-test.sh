#!/bin/bash
set -e

dotnet restore
dotnet publish DotnetApiReference --configuration Release --output binaries
dotnet test DotnetApiReference.Tests --configuration Release
