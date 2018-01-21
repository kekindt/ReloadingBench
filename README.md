# ReloadingBench

Simple ammunition reloading tracking tool for a single loader.

## Features

* Save often used Cartridge information
* Track when lots of cartridges were loaded and spent
* Track how many were loaded in a lot

## Installation

Install Docker and create a MongoDB container.
```
docker run -d -p 27017:27017 --name mongo -v /local/storage/dir:data/db mongo:latest
```

Download and build the ReloadingBench source or run from Docker.

```
docker run -d -p 41277:80 --name reloadingbench -e mongo_url="mongodb://mongo:27017" kekindt/reloadingbench:latest
```

## Feedback

https://github.com/kekindt/ReloadingBench