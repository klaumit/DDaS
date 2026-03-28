#!/bin/sh

OUTDIR=./tutput

rm -Rf $OUTDIR
mkdir $OUTDIR
cp ../src/test.sh $OUTDIR
cp -R ../src/DDaS.Core $OUTDIR
cp -R ../src/DDaS.Tests $OUTDIR
cp -R ../src/DDaS.Server.Lib $OUTDIR
cp -R ../src/DDaS.Tests.Web $OUTDIR
rm -Rf $OUTDIR/DDaS.Tests/TestResults

cd $OUTDIR
dotnet new sln
dotnet sln add DDaS.Tests
dotnet sln add DDaS.Tests.Web

