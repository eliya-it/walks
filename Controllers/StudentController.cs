using Microsoft.AspNetCore.Mvc;
namespace NZWalks.API.Controllers;



[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{


    [HttpGet]
    public IActionResult GetAllStudents()
    {
        string[] studentNames = ["Max", "Alex"];
        return Ok(studentNames);
    }
}
