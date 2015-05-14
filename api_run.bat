SET toolsDir="C:\Users\ataddeini\dev\recipes\tools"
SET srcDir="C:\Users\ataddeini\dev\recipes\src"

cd %toolsDir%\EventStore
start EventStore.ClusterNode.exe --db .\recipes\data --log .\recipes\logs

cd %toolsDir%\MongoDB\Server\3.0\bin
start mongod.exe --dbpath ..\..\..\recipes\data

cd %srcDir%\Recipes.Projections.Host
start dnx . Recipes.Projections.Host
