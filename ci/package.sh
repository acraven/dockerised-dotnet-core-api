#!/bin/bash

docker-compose -f docker-compose.yml build --no-cache
docker-compose -f docker-compose.yml up --exit-code-from tests --abort-on-container-exit

# TODO: upload package
