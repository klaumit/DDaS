#!/bin/sh

curl "http://localhost:5051/api/assemble/ids"
echo ""

curl -v "http://localhost:5051/api/assemble/com/nsm" -F "file=@./hello.asm" -o hello_nsm.com
curl -v "http://localhost:5051/api/assemble/asm/nsm" -F "file=@./hello.com" -o hello_nsm.s

