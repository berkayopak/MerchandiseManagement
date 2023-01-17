
# Merchandise Management API

## What is that?
Proof-of-concept project of e-commerce merchandising management system implementation.

## How to run it?

* First of all, if docker is not installed on your computer, you need to install it.
	* For Windows => https://docs.docker.com/desktop/install/windows-install/
	* For Linux => https://docs.docker.com/desktop/install/linux-install/
	* For Mac => https://docs.docker.com/desktop/install/mac-install/
* After that, you can run ```docker-compose build``` command in the project root folder directory.
* And finally, you can run the ```docker-compose up``` command to get the api up.

## Architecture and project decisions
In general, I can say that it was a project where I tried to write code in accordance with SOLID and DRY principles. Apart from that, I have tried to summarize the parts that I think are important below.

### "Facade" structure
I tried to write simple and well-organized code in accordance with the DDD structure, by passing domain objects as parameters to the "Facade" functions. I preferred to use the "Facade" structure in order to keep the code more tidy and to facilitate maintainability.

### "OperationResult" class
I used this class to keep the communication between client and api stable. Because, thanks to this class, the client side can obtain some standard parameters in almost every condition in the response to be returned.

### "CustomApplicationException" class
I used this class to return a negative response directly to the client from the facade layer in the api. Here, ExceptionHandlerMiddleware catches the thrown exception and turns it into a response suitable for the client and returns to the client.

### HealthChecks
There are situations where we need to monitor the applications we develop with various tools. In these cases, the API you develop can be a real life saver if it provides health check endpoints.

### Swagger
I integrate Swagger into almost every API I develop. I think it is very useful when experimenting with endpoints during the development period.

###  Code-first approach & EF Core & EF Core migrations
I think that writing the code first and then transferring the resulting data and schemas to the database via migration adds order to the backend development. I preferred the EFCore orm package because it provides all kinds of convenience in these parts. Also, its compatibility with Microsoft.AspNetCore.Identity was one of the reasons that pushed me to use EfCore.

### Api versioning
When the request or response content of an endpoint changes, this can sometimes cause client-side corruption. And if the client fails to apply this change to all devices that have access to the app at the same time (for example: mobile apps), this can cause more serious problems. That's why I add versioning to API endpoints. And if there is a need to make changes that can break the client side in this way, I write the same function by increasing its version. Later, when the client-side dev team says it's okay to delete the outdated function, I also delete the outdated function to reduce code pollution. And in this way, we get a more stable API - client interaction.

### Docker & docker-compose
I used docker and docker-compose technologies because of the benefits such as facilitating the first-run process of the API, minimizing the problems that may arise from environment differences, and easily scaling.

### Logging & NLog
I think logging is an indispensable part of almost every project. NLog is a package that I use in all the projects I have developed and that I am satisfied with. Therefore, I used NLog in this project for logging operations.

> :warning: **Note**: Since it is a poc project, I created and hosted the sql server in docker.  And I did not add a fixed volume to keep the data constant/persistent. In some cases, it may cause data loss (For example: deleting the container completely, or turning off the docker engine). It will also be useful to specify a fixed volume on docker-compose.yml when performing these operations on a normal server.