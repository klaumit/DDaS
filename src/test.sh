#!/bin/sh

rm -Rf ./DDaS.Tests/TestResults
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport

