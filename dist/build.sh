#!/bin/sh

echo ::: Building bse image...
docker build -f DDaS.Bse.Dockerfile -t ddas_bse .

echo ::: Building tst image...
./pack.sh
docker build -f DDaS.Tst.Dockerfile -t ddas_tst .

echo ::: Building web image...
echo dotnet publish -c Release -r linux-x64 --sc ../src/DDaS.Server -o output
echo docker build -f DDaS.Web.Dockerfile -t ddas_web .

echo ::: Done.

