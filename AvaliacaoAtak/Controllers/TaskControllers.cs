using Microsoft.AspNetCore.Mvc;
using AvaiacaoAtak.Services;

namespace AvaiacaoAtak.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TasksController : ControllerBase
    {
        private readonly TaskServices _taskService;

        public TasksController(TaskServices taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> Get()
        {
            var tasks = await _taskService.GetAsync();
            return Ok(tasks);
        }

        [HttpGet("{id:length(24)}", Name = "GetTask")]
        public async Task<ActionResult<TaskModel>> Get(string id)
        {
            var task = await _taskService.GetAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetTasksByStatus(string status)
        {
            if (!Enum.TryParse(typeof(Status), status, true, out var parsedStatus))
            {
                return BadRequest("Invalid status provided.");
            }

            var tasks = await _taskService.GetTasksByStatusAsync((Status)parsedStatus);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Create(TaskModel taskModel)
        {
            await _taskService.CreateAsync(taskModel);

            return CreatedAtRoute("GetTask", new { id = taskModel.Id }, taskModel);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TaskModel taskModel)
        {
            var existingTask = await _taskService.GetAsync(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            await _taskService.UpdateAsync(id, taskModel);

            return NoContent();  
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var task = await _taskService.GetAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            await _taskService.RemoveAsync(id);

            return NoContent();
        }

        [HttpGet("DataAsc")]
        public async Task<ActionResult<List<TaskModel>>> GetTasksByCreatedAtAsc()
        {
            var tasks = await _taskService.GetTasksByCreatedAtAscAsync();
            return Ok(tasks);
        }

        [HttpGet("DataDesc")]
        public async Task<ActionResult<List<TaskModel>>> GetTasksByCreatedAtDesc()
        {
            var tasks = await _taskService.GetTasksByCreatedAtDescAsync();
            return Ok(tasks);
        }
    }
}