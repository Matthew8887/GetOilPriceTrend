**How to run the solution?**

Download the project and run it locally. Attached, you will also find a Postman collection with pre-configured requests. In the "Variables" section of the collection, you need to set the `ApiUrl` variable with the value generated from running the solution locally.

**Personal Approach**

I included an initial method called "Status" only to ensure that the solution started without initial errors.

This is my first time working with the RPC protocol. I noticed that Visual Studio provides a specific template for creating a gRPC service, but due to limited time, I preferred to develop everything as a classic web API in .NET 8. 

Having noticed that, in the provided example, the relevant method was passed as a parameter, I decided to use reflection to make everything more dynamic rather than using a simple if/switch statement.

The URL of the price history has been configured as a configuration key of the application, and the list is loaded into a static variable at the application's startup.
