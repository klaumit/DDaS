#!/bin/sh

curl -X POST "http://localhost:5050/api/compile/asm/g16" \
     -F "file=@./hello.c" \
     -o hello.s

curl -X POST "http://localhost:5050/api/compile/com/g16" \
     -F "file=@./hello.c" \
     -o hello.com

