using Microsoft.AspNetCore.Mvc;
using TodoCustomList.Models;
using TodoCustomList.Models.TaskTodo.TaskTodoVM;
using TodoCustomList.Models.Todo.Dto;
using TodoCustomList.Models.Todo.TodoVM;
using TodoCustomList.Services;

namespace TodoCustomList.Controllers
{
    [ApiController()]
    [Route("v1/todo")]
    public class TodoController : ControllerBase
    {
        private TodoService todoService = new TodoService();
        private TaskTodoService taskService = new TaskTodoService();
     
        //create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTodoDTO createTodoDTO)
        {
            try
            {
                var newTodo = await todoService.Create(createTodoDTO);
                
                return StatusCode(StatusCodes.Status201Created, new TodoSumaryResponseViewModel
                {
                    Id = newTodo.Id,
                    Title = newTodo.Title,
                    UserId = newTodo.UserId,
                });
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
                var listTodo = (await todoService.GetAll()).Select(a => new TodoSumaryResponseViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    UserId = a.UserId,
                });

                return StatusCode(StatusCodes.Status200OK, listTodo);
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
                var todo = await todoService.GetById(Guid.Parse(id));
                var listTaskTodo = (await taskService.GetAll()).Where(a => a.TodoId == todo.Id).Select(a => new TaskTodoResponseViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    IsCompleted = a.IsCompleted

                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new GetByIdTodoResponseViewModel
                {
                    Id = todo.Id,
                    Description = todo.Description,
                    Title = todo.Title,
                    Tasks = listTaskTodo
                }); 
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //update
        [HttpPatch()]
        public async Task<IActionResult> Patch([FromBody] UpdateTodoDTO todo)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await todoService.Update(todo));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status304NotModified, ex.Message);
            }
        }

        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await todoService.Delete(Guid.Parse(id));
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
