using System;

namespace CodeHunt.API.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
