#!/bin/sh

echo ::: Building bse image...
docker build -f DDaS.Bse.Dockerfile -t ddas_bse .

echo ::: Building cmd image...
dotnet publish -c Release -r linux-x64 --sc ../src/DDaS.Runner -o output
docker build -f DDaS.Cmd.Dockerfile -t ddas_cmd .

echo ::: Running cmd image...
docker run -it --rm ddas_cmd --help

echo ::: Done.

