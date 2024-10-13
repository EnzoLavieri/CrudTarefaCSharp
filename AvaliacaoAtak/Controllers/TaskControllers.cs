using Microsoft.AspNetCore.Mvc;
using AvaliacaoAtak.Services;
using System.Threading.Tasks;

namespace AvaliacaoAtak.Controllers
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
                return NotFound("Task not founded or invalid Id task number.");
            }

            return Ok(task);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetTasksByStatus(string status)
        {
            if (!Enum.TryParse(typeof(TaskStatus), status, true, out var parsedStatus) || !Enum.IsDefined(typeof(TaskStatus), parsedStatus))
            {
                return BadRequest("Invalid status provided. Accepted values are: 0 = Pending, 1  = InProgress, 2 = Done.");

            }

            var tasks = await _taskService.GetTasksByStatusAsync((TaskStatus)parsedStatus);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Create(TaskModel taskModel)
        {

            if (!Enum.IsDefined(typeof(TaskStatus), taskModel.Status))
            {
                return BadRequest("Wrong status number, use one of these: 0 (Pending), 1 (InProgress) or 2 (Done).");
            }

            await _taskService.CreateAsync(taskModel);

            return CreatedAtRoute("GetTask", new { id = taskModel.Id }, taskModel);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TaskModel taskModel)
        {
            var existingTask = await _taskService.GetAsync(id);

            if (existingTask == null)
            {
                return NotFound("Id is missing or invalid to update.");
            }

            await _taskService.UpdateAsync(id, taskModel);

            return Content($"Task: {id} updated with success.");
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var task = await _taskService.GetAsync(id);

            if (task == null)
            {
                return NotFound("Id is missing or invalid to delete.");
            }

            await _taskService.RemoveAsync(id);

            return Content($"Task: {id} excluded with success.");
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