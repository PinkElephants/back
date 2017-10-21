using System;

namespace Hackinder.Entities.Dto
{
    public class CreateUserDto
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
