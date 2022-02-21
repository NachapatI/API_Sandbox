using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace API_Sandbox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  CommandsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetString()
        {
            return new string[] {"This", "is", "API", "Result"};
        }
    
    }
}