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
docker run -v ./report:/app/coveragereport -it --rm ddas_tst

echo ::: Done.

