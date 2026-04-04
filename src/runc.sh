#!/bin/sh

cd DDaS.Runner

DOC="../../doc"
EXE="dotnet run --"

echo .
$EXE -a compile     -k b20 -m com -i $DOC/hello.c
$EXE -a compile     -k b20 -m asm -i $DOC/hello.c
echo .
$EXE -a compile     -k b30 -m com -i $DOC/hello.c
$EXE -a compile     -k b30 -m asm -i $DOC/hello.c
echo .
$EXE -a compile     -k b31 -m com -i $DOC/hello.c
$EXE -a compile     -k b31 -m asm -i $DOC/hello.c
echo .
$EXE -a compile     -k g16 -m com -i $DOC/hello.c
$EXE -a compile     -k g16 -m asm -i $DOC/hello.c
echo .
$EXE -a compile     -k fpc -m com -i $DOC/hello.pas
$EXE -a compile     -k fpc -m asm -i $DOC/hello.pas
echo .
$EXE -a assemble    -k nsm        -i $DOC/hello.asm
echo .
$EXE -a disassemble -k nsm        -i $DOC/hello_nsm.com
echo .
$EXE -a disassemble -k ice        -i $DOC/hello_nsm.com
echo .
$EXE -a disassemble -k o16        -i $DOC/hello_nsm.com

