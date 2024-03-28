using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PratiseAPI.Controllers
{
    //https://localhost:port/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET : https://localhost:port/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "Adam , Peter , Sam , Billy , Betty ,JC" };

            return Ok(studentNames);
        }
    }
}
