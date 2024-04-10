
## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
  - [Prerequisites](#prerequisites)
  - [APi Usage](#apiusage)


## Introduction

The Currency Conversion API will assist with converting amounts from one currency to another.
The API should support conversions between the following currency pairs: USD, INR, EUR

## Features

List the main features or functionalities of your project.

- Override the json
	Json will be replaced with the value from environment variable.
- Result of the api
	Exchange rate and coverted amount will be displayed as output.


### Prerequisites

.Net framework 6.0 is required for running the application.
Visual studio 2022 is required for opening the project.

### APi Usage

Below are the steps that needs to be followed to run the api application

1 Download the code code from below location,
	https://github.com/AdlinTechnoSpurs/CurrencyConverter.git
2 Click on the CurrencyConversionApi.sln inside the CurrencyConversionApi folder to open the solution in visual studio.
3 Click on "Run" in top of the applcation, it will open a swagger page as shown below
	https://localhost:44375/swagger/index.html
4 Under CurrencyConversion section, Click on the down arrow to see the parameters to be passed as input.
5. Click on "Try it out" to enable the input parameter
6 We will be displayed with sourceCurrency, targetCurrency & amount, enter the respective value and click on "Execute".
7. The result will be displayed as json as shown below.
	{
	  "exchangeRate": 81,
	  "convertedAmount": 40500
	}

	
