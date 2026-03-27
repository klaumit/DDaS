#!/bin/sh

echo ::: Running tst image...
rm -Rf ./report
mkdir ./report
chmod -R 777 ./report
docker run -v ./report:/app/coveragereport -it --rm ddas_tst

echo ::: Running web image...
echo docker run -p 5050:8080 -it --rm ddas_web

echo ::: Done.

