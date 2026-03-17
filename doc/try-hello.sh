#!/bin/sh

curl -X POST "http://localhost:5050/api/code/compile/asm" \
     -F "file=@./hello.c" \
     -o hello.s

curl -X POST "http://localhost:5050/api/code/compile/com" \
     -F "file=@./hello.c" \
     -o hello.com

