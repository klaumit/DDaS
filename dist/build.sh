#!/bin/sh

echo ::: Building bse image...
docker build -f DDaS.Bse.Dockerfile -t ddas_bse .

echo ::: Building tst image...
docker build -f DDaS.Tst.Dockerfile -t ddas_tst .

echo ::: Building web image...
dotnet publish -c Release -r linux-x64 --sc ../src/DDaS.Server -o output
docker build -f DDaS.Web.Dockerfile -t ddas_web .

echo ::: Done.

