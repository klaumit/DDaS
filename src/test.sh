#!/bin/sh

rm -Rf ./DDaS.Tests/TestResults
rm -Rf ./DDaS.Tests.Web/TestResults
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport

