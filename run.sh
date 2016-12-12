#!/bin/bash

id=$(docker ps -aq --filter name=dockerised-dotnet-core-api)

if [ -n "$id" ]; then
  #TODO Need to check if container is running before stopping
  echo "Stopping container (sending SIGINT)"
  docker kill --signal=INT $id
  echo "Removing container"
  docker rm $id
fi

echo Starting new container
docker run \
  --name dockerised-dotnet-core-api \
  -d \
  -p 9000:9000 \
  dockerised-dotnet-core-api/api:latest