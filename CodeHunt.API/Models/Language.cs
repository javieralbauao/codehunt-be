using System;
using System.Collections.Generic;

namespace CodeHunt.API.Models
{
    public class Language
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}
