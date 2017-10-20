using System;
using System.Collections.Generic;

namespace Hackinder.Entities
{
    public class Settings
    {
        public List<string> DesiredSkills { get; set; }
        public List<string> DesiredSpecializations { get; set; }
        public DateTime FromAge { get; set; }
        public DateTime ToAge { get; set; }
    }
}