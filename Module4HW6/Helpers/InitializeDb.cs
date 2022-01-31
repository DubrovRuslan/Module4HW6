using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Module4HW6.Entities;

namespace Module4HW6.Helpers
{
    public class InitializeDb
    {
        private readonly ApplicationDbContext _context;

        public InitializeDb(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTestData()
        {
            var songs = new List<Song>()
            {
                new Song { Title = "Song 1", Duration = new TimeSpan(0, 3, 15), ReleasedDate = new DateTime(2000, 2, 21) },
                new Song { Title = "Song 2", Duration = new TimeSpan(0, 4, 15), ReleasedDate = new DateTime(2001, 2, 22) },
                new Song { Title = "Song 3", Duration = new TimeSpan(0, 5, 5), ReleasedDate = new DateTime(2002, 2, 23) },
                new Song { Title = "Song 4", Duration = new TimeSpan(0, 3, 35), ReleasedDate = new DateTime(2003, 2, 24) },
                new Song { Title = "Song 5", Duration = new TimeSpan(0, 1, 23), ReleasedDate = new DateTime(2004, 2, 25) },
                new Song { Title = "Song 6", Duration = new TimeSpan(0, 3, 15), ReleasedDate = new DateTime(2000, 2, 21) },
                new Song { Title = "Song 7", Duration = new TimeSpan(0, 4, 15), ReleasedDate = new DateTime(2001, 2, 22) },
                new Song { Title = "Song 8", Duration = new TimeSpan(0, 5, 5), ReleasedDate = new DateTime(2002, 2, 23) },
                new Song { Title = "Song 9", Duration = new TimeSpan(0, 3, 35), ReleasedDate = new DateTime(2003, 2, 24) },
                new Song { Title = "Song 10", Duration = new TimeSpan(0, 1, 23), ReleasedDate = new DateTime(2004, 2, 25) }
            };

            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (!_context.Artists.Any())
                    {
                        await _context.Artists.AddAsync(new Artist { Name = "Artist 1", DateOfBirth = new DateTime(2001, 12, 1) });
                        await _context.Artists.AddAsync(new Artist { Name = "Artist 2", DateOfBirth = new DateTime(2002, 12, 2) });
                        await _context.Artists.AddAsync(new Artist { Name = "Artist 3", DateOfBirth = new DateTime(2003, 12, 3) });
                        await _context.Artists.AddAsync(new Artist { Name = "Artist 4", DateOfBirth = new DateTime(2004, 12, 4) });
                        await _context.Artists.AddAsync(new Artist { Name = "Artist 5", DateOfBirth = new DateTime(2005, 12, 5) });
                    }

                    await _context.SaveChangesAsync();
                    if (!_context.Genres.Any())
                    {
                        await _context.Genres.AddAsync(new Genre { Title = "Rock" });
                        await _context.Genres.AddAsync(new Genre { Title = "Pop" });
                        await _context.Genres.AddAsync(new Genre { Title = "Metal" });
                        await _context.Genres.AddAsync(new Genre { Title = "Jazz" });
                        await _context.Genres.AddAsync(new Genre { Title = "Country" });
                    }

                    await _context.SaveChangesAsync();
                    if (!_context.Songs.Any())
                    {
                        var genre = _context.Genres.ToList()[0];
                        var artists = new List<Artist>() { _context.Artists.ToList<Artist>()[0], _context.Artists.ToList<Artist>()[2] };
                        await _context.Songs.AddAsync(new Song() { Title = "Song 1", Duration = new TimeSpan(0, 3, 45), Genre = genre!, GenreId = genre!.Id, Artists = artists, ReleasedDate = new DateTime(2020, 12, 31) });

                        genre = _context.Genres.ToList()[1];
                        artists = new List<Artist>() { _context.Artists.ToList<Artist>()[1] };
                        await _context.Songs.AddAsync(new Song() { Title = "Song 2", Duration = new TimeSpan(0, 3, 15), Genre = genre!, GenreId = genre!.Id, Artists = artists, ReleasedDate = new DateTime(2021, 11, 10) });
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
