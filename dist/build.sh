#!/bin/sh

echo ::: Building bse image...
docker build -f DDaS.Bse.Dockerfile -t ddas_bse .

echo ::: Building tst image...
dotnet publish -c Debug -r linux-x64 ../src/DDaS.Tests -o tutput
cp ../src/test.sh ./tutput
docker build -f DDaS.Tst.Dockerfile -t ddas_tst .

echo ::: Building web image...
echo dotnet publish -c Release -r linux-x64 --sc ../src/DDaS.Server -o output
echo docker build -f DDaS.Web.Dockerfile -t ddas_web .

echo ::: Done.

