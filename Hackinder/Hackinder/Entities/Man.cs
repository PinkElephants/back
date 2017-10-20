using System.Collections.Generic;

namespace Hackinder.Entities
{
    public class Man
    {
        public string Id { get; set; }

        public string Idea { get; set; }

        public OAuthToken Token { get; set; }

        public List<string> Skills { get; set; }
        public List<string> Specializations { get; set; }

        public List<string> Matched { get; set; }
        public List<string> Dismatched { get; set; }
        public List<string> MatchedMe { get; set; } 
        public Settings Settings { get; set; }

    }
}
