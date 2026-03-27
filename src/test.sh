#!/bin/sh

if [ -d "./DDaS.Tests" ]; then
   rm -Rf ./DDaS.Tests/TestResults
   dotnet test --collect:"XPlat Code Coverage"
else
   dotnet test DDaS.Tests.dll --collect:"XPlat Code Coverage"
fi

reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport

