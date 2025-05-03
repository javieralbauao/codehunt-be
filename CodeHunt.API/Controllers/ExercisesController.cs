using CodeHunt.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExercisesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetExercises()
    {
        var exercises = _context.Exercises
            .Select(e => new {
                e.Id,
                e.Title,
                e.Description,
                e.Topic,
                e.Difficulty,
                e.InitialTestData,
                e.FinalTestData,
                e.LanguageId
            })
            .ToList();

        return Ok(exercises);
    }
}
