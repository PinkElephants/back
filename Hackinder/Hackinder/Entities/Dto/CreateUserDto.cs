using System;
using System.Collections.Generic;

namespace Hackinder.Entities.Dto
{
    public class CreateUserDto
    {
        public DateTime BirthDate { get; set; }

        public string Idea { get; set; }
        public List<string> Skills { get; set; }
        public string Summary { get; set; }
    }
}
