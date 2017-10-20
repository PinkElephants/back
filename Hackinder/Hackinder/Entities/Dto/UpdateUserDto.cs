using System.Collections.Generic;

namespace Hackinder.Entities.Dto
{
    public class UpdateUserDto
    {
        public string Idea { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Specializations { get; set; }
    }
}
