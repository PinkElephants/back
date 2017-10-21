using System.Collections.Generic;

namespace Hackinder.Entities.Dto
{
    public class NewMatchDto
    {
        public string user_id { get; set; }
        public List<string> skills { get; set; }
        public string summary { get; set; }
        public string idea { get; set; }
        public bool isMatch { get; set; }
    }
}