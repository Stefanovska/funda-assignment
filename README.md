# Funda assignment - The Agent's App

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)

## General info
In order to list the top 10 agents in Amsterdam third-party service is being used to fetch the properties and find the agents. 
The fetching of the properties happens once the server is started and the top 10 agents are kept in the memory cache,
so it can satisfy a bigger load and the results will be presented to the user. 
Additionally, a periodic service is used that runs every hour and updates the memory cache. 


## Technologies
The project is created with:
* .NET Core 7
* MVC design pattern

## Setup
To run this project, clone it locally and start it using Visual Studio as a standard web application. 