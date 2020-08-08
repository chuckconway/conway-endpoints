# Conway Endpoints

Endpoints is a small library/pattern for defining a single endpoint for a class in Asp.Net Core.  



Tradionally `Controllers` define endpoints through actions (any public method in a `Controller` class), when there is a small number of endpoints this works great, but as the number of actions increases  controllers becomes hard to maintain and to test. 



Endpoint solves this problem by only having one endpoint per class which adheres to the [single responsibility principle](https://en.wikipedia.org/wiki/Single-responsibility_principle). As a side effect you'll have  more files to manage, but we see this as an opportunity to better organize your code giving you more visibility into the application's structure.



## Getting Started

There are 3 use cases for Endpoint: 

1. An endpoint where we know the incoming type and the outgoing type.

2. An endpoint without any incoming type and an outgoing type of `IActionResult`

3. An endpoint where we know the incoming type and an outgoing type of `IActionResult`



### 1. Defining the incoming type and the outgoing type

When you know the incoming and outgoing types. A good example  is when you request save a user to the database and return the newly saved user to the caller.

```c#
public class KnownIncomingAndOutgoingTypesEndpoint : EndpointBase<UserDetailsRequest, UserDetailsResponse> 
{
    /// <summary>
    /// Save the user details
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Saved user.</returns>
    [HttpPost("api/user/save")]
    public override async Task<ActionResult<UserDetailsResponse>> HandleAsync(UserDetailsRequest request)
    {
        return new UserDetailsResponse
        {
            UserId = 2,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName
        };
    }
}
```



### 2. When there is no incoming type

Sometimes requests don't have any data incoming data, it's a simple call to an endpoint. In this case, we don't need an incoming type.



```c#
public class OutgoingTypeOnlyEndpoint : EndpointBase
{
    /// <summary>
    /// Gets the weather for today.
    /// </summary>
    /// <returns></returns>
    [HttpGet("api/us/states")]
    public override async Task<IActionResult> HandleAsync()
    {
      var json =  new JsonResult(new[]
        {
            "Alabama",
            "Alaska",
            "Arizona",
            "Arkansas",
            "California",
            "Colorado",
            "Delaware",
            "Florida"
        });

      return json;
    }
}
```



### 3. When defining an incoming type with the greatest flexibly for the outgoing type 



We don't always know the type we want to return. For example, we might return an array of items under normal circumstances, however, when there is an error we might return a different type with the error details. `IActionResult` allow us this flexibly.



```c#
public class KnownIncomingTypeAndFlexibleOutgoingTypeEndpoint : EndpointBase<SearchRequest>
{
    /// <summary>
    /// Search for most common pets
    /// </summary>
    /// <param name="request">The search term</param>
    /// <returns>Returns results, if no search term is provided an error is returned.</returns>
    [HttpPost("api/search")]
    public override async Task<IActionResult> HandleAsync(SearchRequest request)
    {
        if (!string.IsNullOrEmpty(request.Term))
        {
            //Most common Pets
            var results = new[] {"Cat", "Dog", "Fish", "Chickens", "Hamsters"};

            return new ActionResult<object>(new
            {
                Results = results
            }).Result;
        }

        return new JsonResult(new
        {
            Error = "The search term is missing. Please resubmit with search term",
            ErrorCode = 101
        });
    }
}
```



This project was strongly influenced by [Ardalis's Endpoint](https://github.com/ardalis/ApiEndpoints) project.