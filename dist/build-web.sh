#!/bin/sh

echo ::: Building bse image...
docker build -f DDaS.Bse.Dockerfile -t ddas_bse .

echo ::: Building web image...
dotnet publish -c Release -r linux-x64 --sc ../src/DDaS.Server -o output
docker build -f DDaS.Web.Dockerfile -t ddas_web .

echo ::: Running web image...
docker run -p 5050:8080 -it --rm ddas_web

echo ::: Done.

