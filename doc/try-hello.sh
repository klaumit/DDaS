#!/bin/sh

curl -X POST "http://localhost:5050/api/code/compile" \
     -F "file=@./hello.c" \
     -o hello.s

