using System.Threading.Tasks;
using Conway.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Conway.Endpoint.Examples
{
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
}