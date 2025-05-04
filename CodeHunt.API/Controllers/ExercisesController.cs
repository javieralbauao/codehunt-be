using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeHunt.API.Data;
using CodeHunt.API.Models;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExercisesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public IActionResult CreateExercise([FromBody] Exercise exercise)
    {
        if (exercise == null || string.IsNullOrWhiteSpace(exercise.Title))
        {
            return BadRequest("Datos inválidos.");
        }

        exercise.Id = Guid.NewGuid();
        exercise.CreatedAt = DateTime.UtcNow;

        _context.Exercises.Add(exercise);
        _context.SaveChanges();

        return Ok(new { id = exercise.Id });
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Student")]
    public IActionResult GetExercises()
    {
        var exercises = _context.Exercises
            .Select(e => new
            {
                e.Id,
                e.Title,
                e.Description,
                e.Topic,
                e.Difficulty,
                e.InitialTestData,
                e.FinalTestData,
                e.LanguageId,
                e.CreatedAt
            }).ToList();

        return Ok(exercises);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult DeleteExercise(Guid id)
    {
        var exercise = _context.Exercises.FirstOrDefault(e => e.Id == id);
        if (exercise == null)
            return NotFound();

        var templates = _context.CodeTemplates.Where(t => t.ExerciseId == id).ToList();
        _context.CodeTemplates.RemoveRange(templates);

        _context.Exercises.Remove(exercise);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult UpdateExercise(Guid id, [FromBody] Exercise updated)
    {
        var exercise = _context.Exercises.FirstOrDefault(e => e.Id == id);
        if (exercise == null) return NotFound();

        exercise.Title = updated.Title;
        exercise.Description = updated.Description;
        exercise.Topic = updated.Topic;
        exercise.Difficulty = updated.Difficulty;
        exercise.InitialTestData = updated.InitialTestData;
        exercise.FinalTestData = updated.FinalTestData;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpPost("run")]
    [Authorize(Roles = "Administrator, Student")]
    public IActionResult RunCode([FromBody] RunCodeRequest request)
    {
        var exercise = _context.Exercises.FirstOrDefault(e => e.Id == request.ExerciseId);
        if (exercise == null)
            return NotFound();
        string userOutput = SimulateExecution(request.Code ?? "", exercise.InitialTestData ?? "");
        bool isCorrect = userOutput.Trim() == exercise.FinalTestData?.Trim();

        return Ok(new
        {
            correct = isCorrect,
            userOutput,
            expectedOutput = exercise.FinalTestData
        });
    }

    private string SimulateExecution(string code, string input)
    {
        // Por ahora simplemente devolvemos el "input" como "salida simulada"
        return input; // Esta parte deberías reemplazarla por integración real
    }

    [HttpPost("submit")]
    [Authorize(Roles = "Administrator, Student")]
    public IActionResult SubmitCode([FromBody] SubmitSolutionRequest request)
    {
        var userId = User.Claims.ElementAt(2).Value;
        if (userId == null) return Unauthorized();

        var exercise = _context.Exercises.FirstOrDefault(e => e.Id == request.ExerciseId);
        if (exercise == null) return NotFound();

        var passed = request.Code?.Contains(exercise.FinalTestData  ?? ""); 

        var submission = new Submission
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse(userId),
            ExerciseId = request.ExerciseId,
            Code = request.Code ?? "",
            Passed = passed
        };

        _context.Submissions.Add(submission);
        _context.SaveChanges();

        return Ok(new { message = "Código enviado", passed });
    }
}