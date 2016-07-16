.NET Core Web API Reference
===========================

This repository contains a reference Web API constructed using .NET Core.

`docker-build-and-package-sh` - run a two step process that a) builds and tests the Web and publishes binaries to a volume and b) extracts the volume and builds a lightweight production container.

`docker-run.sh` - run the packaged container.

Linux
-----
When using Linux, use `find . -name '*.sh' | xargs git update-index --chmod=+x` to make the .sh files executable. Boot2Docker doesn't have this problem, but instead sets all files to be executable.