using System.Threading.Tasks;
using Conway.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Conway.Endpoint.Examples
{
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

    public class SearchRequest
    {
        public string Term { get; set; }

        public int ItemsPerPage { get; set; }
    }
}