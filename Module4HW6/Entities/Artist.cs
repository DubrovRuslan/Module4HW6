using System;
using System.Collections.Generic;

namespace Module4HW6.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string InstagramUrl { get; set; }
        public ICollection<Song> Songs { get; set; } = new List<Song>();
    }
}
