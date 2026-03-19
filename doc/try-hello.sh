#!/bin/sh

curl "http://localhost:5050/api/compile/ids"
echo ""

curl "http://localhost:5050/api/compile/asm/g16" -F "file=@./hello.c" -o hello_g16.s
curl "http://localhost:5050/api/compile/com/g16" -F "file=@./hello.c" -o hello_g16.com

curl "http://localhost:5050/api/compile/asm/b31" -F "file=@./hello.c" -o hello_b31.s
curl "http://localhost:5050/api/compile/com/b31" -F "file=@./hello.c" -o hello_b31.com

curl "http://localhost:5050/api/compile/asm/b30" -F "file=@./hello.c" -o hello_b30.s
curl "http://localhost:5050/api/compile/com/b30" -F "file=@./hello.c" -o hello_b30.com

curl "http://localhost:5050/api/compile/asm/b20" -F "file=@./hello.c" -o hello_b20.s
curl "http://localhost:5050/api/compile/com/b20" -F "file=@./hello.c" -o hello_b20.com


