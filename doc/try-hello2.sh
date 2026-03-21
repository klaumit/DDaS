#!/bin/sh

curl "http://localhost:5051/api/assemble/ids"
echo ""

curl "http://localhost:5051/api/assemble/com/nsm" -F "file=@./hello.asm"     -o hello_nsm.com
curl "http://localhost:5051/api/assemble/asm/nsm" -F "file=@./hello_nsm.com" -o hello_nsm.s

