using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using API_Sandbox.Model;

namespace API_Sandbox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendDataController : ControllerBase
    {
        private List<SendData> _data =new List<SendData>
        {
            new SendData {Key = 1, Value = "Test1"},
            new SendData {Key = 2, Value = "Test2"}
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_data);
        }

        [HttpGet("{key}")]
        public IActionResult GetByID(int key)
        {
            var data = _data.Find(x => x.Key == key);
            if(data == null) return NotFound();

            return Ok(data);
        }
    
    }
}