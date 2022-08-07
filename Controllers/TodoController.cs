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
        private UserService userService = new UserService();
     
        //create
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoDTO createTodoDTO)
        {
            try
            {
                var newTodo = await todoService.Create(createTodoDTO);
                
                return StatusCode(StatusCodes.Status201Created, new TodoSumaryResponseViewModel
                {
                    TodoId = newTodo.Id.ToString(),
                    Title = newTodo.Title,
                    UserId = newTodo.UserId.ToString(),
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
                    TodoId = a.Id.ToString(),
                    Title = a.Title,
                    UserId = a.UserId.ToString(),
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

                return StatusCode(StatusCodes.Status200OK, new GetTodoResponseViewModel
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
        [HttpPatch("{todoId}")]
        public async Task<IActionResult> Patch([FromRoute]string todoId, [FromBody]UpdateTodoDTO body)
        {
            try
            {
                await todoService.Update(todoId, body);
                return StatusCode(StatusCodes.Status200OK, true);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status304NotModified, ex.Message);
            }
        }

        [HttpPut("changetodouser/{todoId}")]
        public async Task<IActionResult> ChangeTodoUser([FromRoute]string todoId, [FromBody] TodoChangeUserDto body)
        {
            try
            {
                await todoService.ChangeTodoUser(todoId, body.UserId);
                return StatusCode(StatusCodes.Status200OK, true);
            }
            catch (Exception ex)
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

        [HttpGet("todobyuser")]
        public async Task<IActionResult> GetAllGroupByUser()
        {
            try
            {
                var listTodo =  (todoService.GetAllUserTodo()).GroupBy(a => a.UserId);
                var retornoListTodoUser = new List<TodosUsersResponseViewModel>();

                foreach (var userId in listTodo)
                {
                    var todoResponseViewlModel = new List<TodoResponseViewModel>();
                    var todoRetorno = new TodosUsersResponseViewModel();

                    var userName = (await userService.GetById(Guid.Parse(userId.Key))).Name;

                    foreach (var todoByUser in userId)
                    {
                        if(todoByUser.TodoId is not null)
                        {
                            todoResponseViewlModel.Add(new TodoResponseViewModel(){ Id = todoByUser.TodoId, Title = todoByUser.Title });
                        }
                    }

                    todoRetorno.UserId =userId.Key;
                    todoRetorno.UserName = userName;
                    todoRetorno.TodoList = todoResponseViewlModel;

                    retornoListTodoUser.Add(todoRetorno);

                }

                return StatusCode(StatusCodes.Status200OK, retornoListTodoUser);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
