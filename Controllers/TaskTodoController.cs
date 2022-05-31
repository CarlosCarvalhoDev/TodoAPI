using Microsoft.AspNetCore.Mvc;
using TodoCustomList.Models.TaskTodo.TaskTodoDTO;
using TodoCustomList.Models.TaskTodo.TaskTodoVM;
using TodoCustomList.Services;

namespace TodoCustomList.Controllers
{
    [ApiController()]
    [Route("v1/task")]
    public class TaskTodoController : ControllerBase
    {
        private TaskTodoService taskTodoService = new TaskTodoService();
        private UserService userService = new UserService();
        //create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTaskTodoDTO createTaskTodoDTO)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, await taskTodoService.Create(createTaskTodoDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //read all
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await taskTodoService.GetAll());
            }
            catch
            {
                return NoContent();
            }
        }

        //read
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await taskTodoService.GetById(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        //update
        [HttpPatch()]
        public async Task<IActionResult> Patch([FromBody] UpdateTaskTodoDTO todo)
        {
            try
            {
                var taskTodoModel = await taskTodoService.Update(todo);

                return StatusCode(StatusCodes.Status200OK, new UpdateTaskTodoResponseViewModel()
                {
                    Id = taskTodoModel.Id,
                    IsCompleted = taskTodoModel.IsCompleted,
                    TodoId = taskTodoModel.TodoId,
                    Name = taskTodoModel.Name,
                }); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status304NotModified, ex);
            }
        }

        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await taskTodoService.Delete(Guid.Parse(id));
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
