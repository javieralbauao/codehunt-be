// ExercisesController.cs
using CodeHunt.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly AppDbContext _context;

    public TemplatesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{exerciseId}/template/{languageId}")]
    public IActionResult GetTemplate(Guid exerciseId, Guid languageId)
    {
        var template = _context.CodeTemplates
            .FirstOrDefault(t => t.ExerciseId == exerciseId && t.LanguageId == languageId);

        if (template == null) return NotFound("No hay plantilla para este ejercicio y lenguaje");

        return Ok(new { template = template.Template });
    }
}