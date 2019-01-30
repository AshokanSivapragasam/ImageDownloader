start D:\Usr\Ashok\Projects\MongoDb\Server\3.4\bin\mongod.exe --dbpath "D:\Usr\Ashok\Projects\MongoDb\Data"
TIMEOUT /T 5

start D:\Usr\Ashok\Projects\MongoDb\Server\3.4\bin\mongo.exe
TIMEOUT /T 5

start cmd /K "cd /d D:\Usr\Ashok\Projects\stack-overflow-webapi && npm start"
TIMEOUT /T 5

start cmd /K "cd /d D:\Usr\Ashok\Projects\stack-overflow-mean && npm start"
TIMEOUT /T 5