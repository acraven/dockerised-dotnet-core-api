#!/bin/bash

docker-compose -f docker-compose.yml up --build --exit-code-from tests --abort-on-container-exit
