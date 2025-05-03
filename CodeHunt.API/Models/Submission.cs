using System;

namespace CodeHunt.API.Models
{
    public class Submission
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ExerciseId { get; set; }
        public string Code { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool? Passed { get; set; }

        public User User { get; set; }
        public Exercise Exercise { get; set; }
    }
}
