#!/bin/bash

check_errors()
{
  if [ "${1}" -ne "0" ]; then
    echo "'${2}' FAILED WITH ERROR CODE ${1}"
    exit ${1}
  fi
}

#TODO: Enable Debug/Release configurations

dotnet restore
check_errors $? "dotnet restore"

dotnet publish src/DotnetApiReference.Api --configuration Release --output package
check_errors $? "dotnet publish"

dotnet test src/DotnetApiReference.Api.Tests --configuration Release
check_errors $? "dotnet test"
