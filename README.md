# Weazor
Weazor is an application that shows weather written in C#. It was written as a submission to Code2gether August challenge (GUI application that uses some API).

It is divided into two parts:
- WeazorWebsite
- WeazorApi

WeazorWebsite is a Blazor WebAssembly application. It contains all frontend logic and some SPA routing.
WeazorApi is an ASP.NET Core application that acts like a caching service: all requests are cached for an hour to minimize OpenWeatherMap API calls. 
The API key is stored in appsettings.json.

#Installation
As the application consists of two parts, you need to setup both of them to be able to use the app. 
First, you need to start WeazorApi.
After that you need to configure WeazorWebsite appsettings.json and add API url here. Then the app will be ready to use!
