using Microsoft.AspNetCore.Mvc;
using TodoCustomList.Models;
using TodoCustomList.Models.Todo.Dto;
using TodoCustomList.Services;

namespace TodoCustomList.Controllers
{
    [ApiController()]
    [Route("v1/todo")]
    public class TodoController : ControllerBase
    {
        private TodoService todoService = new TodoService();

        //create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTodoDTO createTodoDTO)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, await todoService.Create(createTodoDTO));
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
                return StatusCode(StatusCodes.Status200OK, await todoService.GetAll());
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
                return StatusCode(StatusCodes.Status200OK, await todoService.GetById(Guid.Parse(id)));
            }
            catch(Exception ex)
            {
                return NotFound(ex);
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
                return StatusCode(StatusCodes.Status304NotModified, ex);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
