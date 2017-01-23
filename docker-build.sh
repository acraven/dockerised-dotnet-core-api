#!/bin/bash
set -e

location="`dirname \"$0\"`"

docker build --tag=dockerised-dotnet-core-api/build:latest --file $location/app/Dockerfile.build --no-cache=true $location/app