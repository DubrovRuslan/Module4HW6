using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Module4HW6.Entities;

namespace Module4HW6.Helpers
{
    public class Requests
    {
        private readonly ApplicationDbContext _context;

        public Requests(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Request01()
        {
            var songs = new List<Song>();
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    songs = await _context.Songs
                    .Include(s => s.Artists)
                    .Include(s => s.Genre).ToListAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }

            foreach (var song in songs)
            {
                foreach (var artist in song.Artists)
                {
                    Console.WriteLine($"{artist.Name} {song.Title} {song.Genre.Title}");
                }
            }
        }

        public async Task Request02()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = _context.Songs.Include(s => s.Genre)
                    .AsEnumerable()
                    .GroupBy(s => s.Genre.Title).Select(s => new { Title = s.Key, Count = s.Count() }).AsEnumerable();
                    foreach (var item in result)
                    {
                        Console.WriteLine($"{item.Title} -> {item.Count} songs");
                    }

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task Request03()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await _context.Songs.Where(s => s.ReleasedDate < _context.Artists.Min(a => a.DateOfBirth)).Select(s => s).ToListAsync();

                    foreach (var item in result)
                    {
                        Console.WriteLine(item.Title);
                    }

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
