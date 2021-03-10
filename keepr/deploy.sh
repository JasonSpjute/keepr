dotnet publish -c Release
docker build -t keepr ./bin/Release/net5.0/publish
docker tag keepr registry.heroku.com/keepr25/web
docker push registry.heroku.com/keepr25/web
heroku container:release web -a keepr25
echo press any key
read end