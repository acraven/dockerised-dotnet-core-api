#!/bin/bash

id=$(docker ps -aq --filter name=dotnet-api-reference-api)

if [ -n "$id" ]; then
  #TODO Need to check if container is running before stopping
  echo "Stopping container (sending SIGINT)"
  docker kill --signal=INT $id
  echo "Removing container"
  docker rm $id
fi

echo Starting new container
docker run \
  --name dotnet-api-reference-api \
  -itd \
  -p 9000:9000 \
  dotnet-api-reference/api:latest