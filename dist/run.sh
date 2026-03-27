#!/bin/sh

echo ::: Running tst image...
docker run              -it --rm ddas_tst

echo ::: Running web image...
docker run -p 5050:8080 -it --rm ddas_web

echo ::: Done.

