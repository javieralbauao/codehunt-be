using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeHunt.API.Data;
using CodeHunt.API.Models;

[ApiController]
[Route("api/[controller]")]
public class CodeTemplatesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CodeTemplatesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public IActionResult CreateTemplate([FromBody] CodeTemplate template)
    {
        if (template == null || string.IsNullOrWhiteSpace(template.Template))
        {
            return BadRequest("Plantilla inválida.");
        }

        template.Id = Guid.NewGuid();
        _context.CodeTemplates.Add(template);
        _context.SaveChanges();

        return Ok(new { id = template.Id });
    }

    [HttpGet("{exerciseId}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult GetTemplatesByExercise(Guid exerciseId)
    {
        var templates = _context.CodeTemplates
            .Where(t => t.ExerciseId == exerciseId)
            .Select(t => new
            {
                t.LanguageId,
                t.Template
            }).ToList();

        return Ok(templates);
    }

    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public IActionResult UpdateTemplate([FromBody] CodeTemplate template)
    {
        var existing = _context.CodeTemplates
            .FirstOrDefault(t => t.ExerciseId == template.ExerciseId && t.LanguageId == template.LanguageId);

        if (existing != null)
        {
            existing.Template = template.Template;
        }
        else
        {
            template.Id = Guid.NewGuid();
            _context.CodeTemplates.Add(template);
        }

        _context.SaveChanges();
        return NoContent();
    }
}