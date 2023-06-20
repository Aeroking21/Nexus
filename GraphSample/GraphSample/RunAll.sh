#!/bin/sh

# Change directory to GraphSample
cd ./GraphSample

# Run dotnet watch for the frontend in the background
dotnet watch &

# Change directory to WebBackend
cd ./WebBackend

# Run dotnet run --launch-profile https for the backend in the foreground
dotnet run --launch-profile https
