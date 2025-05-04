using System;

namespace CodeHunt.API.Models
{
    public class RunCodeRequest
    {
        public Guid ExerciseId { get; set; }
        public string? Language { get; set; }
        public string? Code { get; set; }
    }
}
