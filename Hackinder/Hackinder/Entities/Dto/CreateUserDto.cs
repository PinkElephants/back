using System;
using System.Collections.Generic;

namespace Hackinder.Entities.Dto
{
    public class CreateUserDto
    {
        public string idea { get; set; }
        public List<string> skills { get; set; } = new List<string>();
        public string summary { get; set; }
    }
}
