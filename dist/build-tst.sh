#!/bin/sh

echo ::: Building bse image...
docker build -f DDaS.Bse.Dockerfile -t ddas_bse .

echo ::: Building tst image...
./pack-tst.sh
docker build -f DDaS.Tst.Dockerfile -t ddas_tst .

echo ::: Running tst image...
rm -Rf ./report
mkdir ./report
chmod -R 777 ./report
mkdir ./nuget
chmod -f -R 777 ./nuget
docker run -v ./report:/app/coveragereport -v ./nuget:/home/appusr/.nuget -it --rm ddas_tst

echo ::: Done.

