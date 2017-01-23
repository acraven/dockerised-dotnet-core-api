#!/bin/bash
set -e

id=$(docker ps -aq --filter name=dockerised-dotnet-core-api)

if [ -n "$id" ]; then
  isrunning=$(docker inspect --format="{{ .State.Running }}" $id)

  if [ "$isrunning" == "true" ]; then
    echo "Stopping container (sending SIGINT)"
    docker kill --signal=INT $id
  fi

  echo "Removing container"
  docker rm $id
fi

echo "Starting new container"
docker run \
  --name dockerised-dotnet-core-api \
  -d \
  -p 9000:9000 \
  dockerised-dotnet-core-api/api:latest