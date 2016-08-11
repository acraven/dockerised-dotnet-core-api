#!/bin/bash

docker run \
  --name dotnet-api-reference \
  -itd \
  -p 9000:9000 \
  dotnet-api-reference/api:latest
