# d3vs1m
#### :construction: under construction :construction:

##### d3vs1m - discrete events & development for network device simulation

![Build and Test](https://github.com/adriansinger87/d3vs1m/workflows/Build%20and%20Test/badge.svg)

## Introduction
This solultion will provide an extensible and highly configuralbe **discrete event simulator**. The current implementation of different simulators are supposed to realize a simulation for wireless sensor networks in a 3D environment.

The provided simulation models are shown in the following list.
The checked models are basically runnalbe or under development 
- [x] scene model
- [x] network model
- [x] antenna model
- [x] channel model (radio channel)
- [x] device model
- [ ] communication model
- [x] energy model
 
## Prerequisites

To work with this repository you need
* at least **.NET Core 2.1** to build and run the web-application
* all other assemblies are of the project type **.NET Standard 2.0** to ensure max. compatibility. 

## Development

The project can be developed with
* at least **Visual Studio 2017** or with 
* **Visual Studio Code.** In this case there are some extensions like C# required.

## Test and Running

* The web-application is **build and tested** under Windows, Linux and Mac.
* To **start the system locally** (in your IDE), just hit the run or debug button and
  your browser should open the home-page. 
* To host the application we recommend to use a web-server like IIS or nginx as a reverse proxy and
  install the kestrel server behind the public web-server.
  For more information take a look into the [Microsoft-Docs](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/?view=aspnetcore-2.2) 

## Acknowledgement

I want to thank all collaborators and the [Fraunhofer IWU](https://www.iwu.fraunhofer.de/)
for the support on this project. :beers: :beers:
