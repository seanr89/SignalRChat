# SignalRChat
Test App to configure Signal R

## App Structure
Two projects make this up currently:

- SignalServer
- ChatConsole

## Build and Publish
run cmd `dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true`

## Docker
Containerisation could be used to make deployment easier for the server itself
Additionally, Redis could be interesting to review for chat history

### Commands
1. build: `docker build -t signalserver .`

2. run: `docker run signalserver`
or `docker run -d -p 8080:80 --name signaler signalserver`