#!/bin/sh

dotnet publish -c Release -r linux-x64 --sc ../src/DDaS.Server -o output

docker build -f DDaS.Dockerfile -t ddas_srv .


