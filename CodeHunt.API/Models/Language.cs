using System;
using System.Collections.Generic;

namespace CodeHunt.API.Models
{
    public class Language
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

    }
}
