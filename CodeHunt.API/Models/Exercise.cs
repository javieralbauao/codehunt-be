using System;
using System.Collections.Generic;

namespace CodeHunt.API.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Topic { get; set; }
        public string? Difficulty { get; set; }
        public string? InitialTestData { get; set; }
        public string? FinalTestData { get; set; }
        public Guid LanguageId { get; set; }
        public DateTime CreatedAt { get; set; }

        public required Language Language { get; set; }
        public required ICollection<TestCase> TestCases { get; set; }
        public ICollection<Submission>? Submissions { get; set; }
    }
}
