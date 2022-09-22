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