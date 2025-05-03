using System;

namespace CodeHunt.API.Models
{
    public class TestCase
    {
        public Guid Id { get; set; }
        public Guid ExerciseId { get; set; }
        public string? InputData { get; set; }
        public string? ExpectedOutput { get; set; }
        public bool IsPublic { get; set; }

        public Exercise? Exercise { get; set; }
    }
}
