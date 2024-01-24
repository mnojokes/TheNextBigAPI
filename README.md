# TheNextBigAPI

## What is it?
Have you ever wondered what it would be like to have a currency rate API that has currency data just up to 2014? Well, wonder no more, because it is here!

## How to use it?
* Open the solution in Visual Studio;
* Modify connection string in appsettings.json to match the local database on your machine;
* Run and enjoy the amazing currency rates from before 2014!

## How it works?
The app accepts the date from the user and fetches the currency rate changes compared with the previous day.  
Rates are sorted in descending order by their absolute values (the bigger the change, the higher it is, no matter positive or negative).  
The app uses a Postgre database for local cache to minimize web API calls. When the user requests currency rate data from a particular date, the retrieves the data either from its internal cache or the web.

## Limitations
No currency data available after the year 2014.

## Niceties
* It has a running background service that cleans up cache every 24 hours (can be modified in appsettings.json);
* It works entirely with XML (probably not that nice, but it was for bonus points);
* It has the most amazing Swagger documentation seen anywhere in the world (probably).
