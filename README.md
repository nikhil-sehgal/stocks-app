
## Summary:

* Stocks is a full stack web application, which shows current prices of stocks like Bitcoin, Ethereum and some Indian companies stocks.
* Application backend is C# based asp.net web api.
* Application front end is of 2 types
	* AngularJS based.
	* Jquery Based.
* This appliaction use lots of functionality like
	* Parellel Programming.
	* Caching.
	* Locks.
	* Background workers.
	* Async function calls.
	* Delegates.

## Dependencies

* Install using nuget manager
	* CORS
* Present in stocksFrontend
	* angular.min.js
	* jquery-1.10.2.min.js
	* bootstrap.min.js
	* bootstrap.min.css  

## Using the project files:

* #### Backend WebApi
1. Create a new web api project in visual studio.
2. Copy and paste the codes from the stockBackend  files to the respective folders in web api solution. 
	* ###### Be careful while pasting and not to mess up with the namespaces and copy only classes. 
3. Once done build and solve the errors.
4. Start the project using ctrl+f5 after the successful build.
5. Store the URL from backend.

* #### Frontend 
1. Create new empty web application.
2. Copy and paste folders from StocksFrontEnd.
3. Change the url angular frontend and jquery frontend from the copied url of the backend 5th step.
	* Angular one's will be changed in Angular/Services **
		* bitcoin.service.js
		* ethereum.service.js
		* stocks.service.js
	* Jquery one will be changed in Jquery/Scripts
		* StocksGSpinner.scripts.js

## Running Website Demo


#### Angular frontend
* [Angular FrontEnd 1](http://nikhilsehgal.us-west-2.elasticbeanstalk.com/StocksFrontEndHtml/Angular/stocks.html).

* [Angular FrontEnd 2](http://nikhilsehgal.us-west-2.elasticbeanstalk.com/StocksFrontEndHtml/Angular/stocks2.html).

#### Jquery frontend 
* [Jquery FrontEnd ](http://nikhilsehgal.us-west-2.elasticbeanstalk.com/StocksFrontEndHtml/Jquery/Terminal.html).
