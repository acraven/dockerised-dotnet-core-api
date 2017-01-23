#!/bin/bash
set -e

location="`dirname \"$0\"`"

#Remove the artifacts extracted from the last build
rm -f $location/package/binaries.tar

echo "Building application binaries"
docker build --tag=dockerised-dotnet-core-api/build:latest --file $location/app/Dockerfile.build --no-cache=true $location/app

echo "Extracting binaries"
#Docker Run can't be used as the volume is mounted in boot2docker.
id=$(docker create dockerised-dotnet-core-api/build:latest)
docker cp $id:/build/binaries/. - > $location/package/binaries.tar
docker rm -v $id

#TODO: Check binaries.tar exists and return error code if not

echo "Packaging binaries"
docker build --tag=dockerised-dotnet-core-api/api:latest --file $location/package/Dockerfile.package --no-cache=true $location/package