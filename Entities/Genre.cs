﻿namespace Module4HW6.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
