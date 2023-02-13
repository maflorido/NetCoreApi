using Agenda.Domain.Requests;
using Agenda.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service) 
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<IActionResult> Get() 
        {            
            var tasks = await _service.Get();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostUserRequest request)
        {
            await _service.New(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutUserRequest request)
        {
            await _service.Save(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
