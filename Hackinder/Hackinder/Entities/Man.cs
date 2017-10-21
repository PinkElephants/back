using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Hackinder.Entities
{
    public class Man
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string AuthKey { get; set; }

        public string Idea { get; set; }
        public List<string> Skills { get; set; }
        public List<string> LowerSkills { get; set; }

        public string Summary { get; set; }

        public List<string> Matched { get; set; } = new List<string>();
        public List<string> Dismatched { get; set; } = new List<string>();
        public List<string> MatchedMe { get; set; } = new List<string>();

        public Settings Settings { get; set; }
        
    }
}
