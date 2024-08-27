API Aggregation Service Documentation

Overview
The API Aggregation Service is designed to aggregate data from multiple external APIs and provide a unified response. It allows clients to fetch combined data from various sources, with options to filter and sort the data according to specific parameters.

~Setup & Configuration

1. Prerequisites
 .NET Core SDK installed on your machine.
 A valid API key for the News API, which can be obtained from NewsAPI.

2. Configuration
 API URLs: Ensure the base URLs for the external APIs are correctly set in the service.
 Phone Data API: https://api.restful-api.dev/objects
 News Data API: https://newsapi.org/v2/top-headlines?country=us&apiKey={Your-API-Key}
 JSONPlaceholder API: https://jsonplaceholder.typicode.com/posts
 Logging: Logging is configured using Microsoft.Extensions.Logging. By default, logs will be output to the console and debug window. You can add additional logging providers like Serilog or NLog as needed.

3. Running the Service
 Development Environment:
 Use dotnet run from the project directory to start the service.
 Access the service via http://localhost:5000/api/aggregation.

-Production Environment:
 Deploy the service to your preferred hosting environment (e.g., Azure, AWS, IIS).
 Ensure environment variables for API keys and configuration settings are correctly set.

4. Testing the API
 Swagger: The service includes Swagger for API testing and documentation.
 Navigate to http://localhost:5000/swagger to access the Swagger UI.

5. Error Logs
 By default, logs are output to the console and debug output. In a production environment, consider configuring logging to write to a file or a log aggregation service.

~Dependencies
 Newtonsoft.Json: Used for serializing and deserializing JSON responses.
 Microsoft.Extensions.Http: For managing HTTP client instances.
 Microsoft.Extensions.Logging: For logging error messages and diagnostic information.

~Endpoints
1. GET /api/aggregation
Description
This endpoint retrieves aggregated data from the following APIs:

-Phone Data API: Fetches phone models data.
-News Data API: Fetches the latest news articles.
-JSONPlaceholder API: Fetches sample posts data.

-Query Parameters
Parameter	     Type	   Description
filterByTitle	 string	Filters articles and posts by title. Partial matches are supported.
filterByAuthor	string	Filters news articles by author. Partial matches are supported.
filterByName	  string	Filters phone data by name. Partial matches are supported.
sortBy		       string	Specifies the field to sort the data by. Supported values: title, author, publishedAt, name, capacity.
sortDescending	 bool	 If true, sorts the data in descending order. Defaults to false for ascending order.

-Response Format
Status Code: 200 OK
Content Type: application/json

-Fallback Behavior
If any of the external APIs are unavailable, the service will return a fallback response. This could include default data, such as an empty list or placeholder entries, to ensure the service still returns a valid response.

Error Handling
500 Internal Server Error: Returned if the service encounters an unexpected error.
400 Bad Request: Returned if query parameters are invalid.


~Conclusion
The API Aggregation Service provides a unified interface to fetch and manipulate data from multiple external APIs. It offers filtering, sorting, and fallback mechanisms to ensure robust and reliable data aggregation. This documentation should assist you in understanding, configuring, and utilizing the service effectively.

