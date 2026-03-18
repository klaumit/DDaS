#!/bin/sh

curl -X POST "http://localhost:5050/api/compile/asm/g16" \
     -F "file=@./hello.c" \
     -o hello_g16.s

curl -X POST "http://localhost:5050/api/compile/com/g16" \
     -F "file=@./hello.c" \
     -o hello_g16.com

curl -X POST "http://localhost:5050/api/compile/asm/b31" \
     -F "file=@./hello.c" \
     -o hello_b31.s

curl -X POST "http://localhost:5050/api/compile/com/b31" \
     -F "file=@./hello.c" \
     -o hello_b31.com

