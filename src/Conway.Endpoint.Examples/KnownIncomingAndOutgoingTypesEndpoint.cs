using System.Threading.Tasks;
using Conway.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Conway.Endpoint.Examples
{
    public class UserDetailsResponse
    {
        public int UserId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string MiddleName { get; set; }
    }

    public class UserDetailsRequest
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string MiddleName { get; set; }
    }
    
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
}