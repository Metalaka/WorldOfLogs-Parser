# WorldOfLogs-Parser

## Description

This tool allow you to store in a database the javascript data gathered from a world of logs expression editor backup. 

## Run/config

Use `Data/appsettings.sample.json` to create the configuration file `appsettings.json`

build and run with the path argument: `dotnet FileImporter.dll --path="/path/to/backup/"`

Every html files of that folder will be parsed and imported to the db.

## Context

World of Logs was an online service that allow to analyse logs of World of Warcraft fights.
http://www.worldoflogs.com/ has been shut down on 2021-05-17.

It was the last place were logs of WOW: WOTLK expansion can still be accessible and can be uploaded.
These two points are necessary to compare behavior of different versions of a similar fight.
 
### Analyse

WOL allow to upload *reports* organised by *guilds* but not to download reports.
There where many way to analyse each report
 - by actor (player, creature, boss)
 - by spell
 - with death log
 - expression editor: ability to query the whole log with conditions/expressions

The expression editor can be used without conditions to see all lines of the report.
So with some time, a complete report can be downloaded with a web crawler.

Moreover the expression editor UI was mainly build on the client-side with raw data injected inside the html sources.
The raw json data is pretty simply attainable and so there is no need to parse formatted html to rebuild the data!

After backing-up a report, data have to be extracted from the html files.
I would like to exploit the data through a database so it's why this project target that output.

