#!/bin/bash

check_errors()
{
  if [ "${1}" -ne "0" ]; then
    echo "'${2}' FAILED WITH ERROR CODE ${1}"
    exit ${1}
  fi
}

location="`dirname \"$0\"`"

docker build --tag=dockerised-dotnet-core-api/build:latest --file $location/app/Dockerfile.build --no-cache=true $location/app
check_errors $? "docker build app"

rm -f $location/package/binaries.tar

#Extract the contents of the package folder of the build image. Docker Run can't be
#used as the volume is mounted in boot2docker.
id=$(docker create dockerised-dotnet-core-api/build:latest)
docker cp $id:/build/package/. - > $location/package/binaries.tar
docker rm -v $id

#TODO: Check package.tar exists and return error code if not

docker build --tag=dockerised-dotnet-core-api/api:latest --file $location/package/Dockerfile.package --no-cache=true $location/package
check_errors $? "docker build package"