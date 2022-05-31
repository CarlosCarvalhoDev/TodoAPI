using Microsoft.AspNetCore.Mvc;
using TodoCustomList.Models;
using TodoCustomList.Models.User.Dto;
using TodoCustomList.Services;

namespace TodoCustomList.Controllers
{
    [ApiController()]
    [Route("v1/user")]
    public class UserController : ControllerBase
    {
        private UserService userService = new UserService();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, await userService.CreateUser(createUserDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await userService.GetAll());
            }
            catch
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await userService.GetById(Guid.Parse(id)));
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPatch()]
        public async Task<IActionResult> Patch([FromBody] UserModel user)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, await userService.Update(user));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status304NotModified, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await userService.Delete(Guid.Parse(id));
                return StatusCode(StatusCodes.Status200OK);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            
        }

        


    }
}
