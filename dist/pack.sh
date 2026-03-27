#!/bin/sh

OUTDIR=./tutput

rm -Rf $OUTDIR
mkdir $OUTDIR
cp ../src/test.sh $OUTDIR
cp -R ../src/DDaS.Core $OUTDIR
cp -R ../src/DDaS.Tests $OUTDIR
rm -Rf $OUTDIR/DDaS.Tests/TestResults

cd $OUTDIR
dotnet new sln
dotnet sln add DDaS.Tests

