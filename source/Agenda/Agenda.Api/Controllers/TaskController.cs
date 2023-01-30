using Agenda.Domain.Requests;
using Agenda.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service) 
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
        public async Task<IActionResult> Post([FromBody] PostTaskRequest request)
        {
            await _service.New(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PutTaskRequest request)
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
