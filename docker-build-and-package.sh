#!/bin/bash

check_errors()
{
  if [ "${1}" -ne "0" ]; then
    echo "'${2}' FAILED WITH ERROR CODE ${1}"
    exit ${1}
  fi
}

docker build --tag=dotnet-api-reference/build:latest --file build/Dockerfile.build --no-cache=true build
check_errors $? "docker build"

mkdir -p package
rm -f package/binaries.tar

#Extract the contents of the package folder of the build image. Docker Run can't be
#used as the volume is mounted in boot2docker.
id=$(docker create dotnet-api-reference/build:latest)
docker cp $id:/build/package/. - > package/binaries.tar
docker rm -v $id

#TODO: Check package.tar exists and return error code if not

docker build --tag=dotnet-api-reference/api:latest --file package/Dockerfile.package --no-cache=true package