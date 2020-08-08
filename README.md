# Conway Endpoints

Endpoints is a small library/pattern for defining a single endpoint in Asp.Net Core.  



Traditionally, controllers define endpoints through actions (any public method in a `Controller` class); when there are a small number of endpoints, this works great, but as the number of actions increases, controllers become hard to maintain and test. 



Endpoint solves this problem by only having one endpoint per class, which adheres to the [single responsibility principle](https://en.wikipedia.org/wiki/Single-responsibility_principle). As a side-effect, you'll have more files to manage, but we see this as an opportunity to organize your code, thus allowing you more visibility into the application's structure.



## Getting Started

There are 3 implementations of Endpoints: 

1. An endpoint where we know the incoming type and the outgoing type.

2. An endpoint without any incoming type and an outgoing type of `IActionResult`

3. An endpoint where we know the incoming type and an outgoing type of `IActionResult`



### 1. Defining the incoming type and the outgoing type

When the incoming and outgoing types are known.



For example,  user details are posted to an endpoint and the endpointed returns the saved user to the caller.

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

Sometimes requests don't have any data incoming data, it's a simple call to an endpoint. It might be a trigger or request static data,  in this case, we don't need an incoming type.



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



We don't always know our return type. For example, we might return an array of items under normal circumstances, however, in the event of an error we might return a different type. `IActionResult` allow us this flexibly.



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



This project was inspired by [Ardalis's Endpoint](https://github.com/ardalis/ApiEndpoints) project.