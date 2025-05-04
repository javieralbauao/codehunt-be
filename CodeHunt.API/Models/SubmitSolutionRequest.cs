public class SubmitSolutionRequest
{
    public Guid ExerciseId { get; set; }
    public string? Language { get; set; }
    public string? Code { get; set; }
}