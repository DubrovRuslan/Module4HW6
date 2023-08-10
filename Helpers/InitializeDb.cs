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
            var genres = new List<Genre>()
            {
                new Genre { Title = "Rock" },
                new Genre { Title = "Pop" },
                new Genre { Title = "Metal" },
                new Genre { Title = "Jazz" },
                new Genre { Title = "Country" }
            };
            var songs = new List<Song>()
            {
                new Song { Title = "Song 1", Duration = new TimeSpan(0, 3, 15), ReleasedDate = new DateTime(2000, 2, 21), Genre = genres[0] },
                new Song { Title = "Song 2", Duration = new TimeSpan(0, 4, 15), ReleasedDate = new DateTime(2001, 2, 22), Genre = genres[1] },
                new Song { Title = "Song 3", Duration = new TimeSpan(0, 5, 5), ReleasedDate = new DateTime(2002, 2, 23), Genre = genres[2] },
                new Song { Title = "Song 4", Duration = new TimeSpan(0, 3, 35), ReleasedDate = new DateTime(2003, 2, 24), Genre = genres[3] },
                new Song { Title = "Song 5", Duration = new TimeSpan(0, 1, 23), ReleasedDate = new DateTime(2004, 2, 25), Genre = genres[4] },
                new Song { Title = "Song 6", Duration = new TimeSpan(0, 3, 15), ReleasedDate = new DateTime(2000, 2, 21), Genre = genres[1] },
                new Song { Title = "Song 7", Duration = new TimeSpan(0, 4, 15), ReleasedDate = new DateTime(2001, 2, 22), Genre = genres[2] },
                new Song { Title = "Song 8", Duration = new TimeSpan(0, 5, 5), ReleasedDate = new DateTime(2002, 2, 23), Genre = genres[4] },
                new Song { Title = "Song 9", Duration = new TimeSpan(0, 3, 35), ReleasedDate = new DateTime(2003, 2, 24), Genre = genres[3] },
                new Song { Title = "Song 10", Duration = new TimeSpan(0, 1, 23), ReleasedDate = new DateTime(2004, 2, 25), Genre = genres[1] }
            };
            var artists = new List<Artist>()
            {
                new Artist { Name = "Artist 1", DateOfBirth = new DateTime(2001, 12, 1), Email = "Artist1@gmail.com", InstagramUrl = "#artist1", Phone = "+380978123123", Songs = new List<Song> { songs[0], songs[1] } },
                new Artist { Name = "Artist 2", DateOfBirth = new DateTime(2002, 12, 2), Email = "Artist2@gmail.com", InstagramUrl = "#artist2", Phone = "+380978123123", Songs = new List<Song> { songs[2], songs[3] } },
                new Artist { Name = "Artist 3", DateOfBirth = new DateTime(2003, 12, 3), Email = "Artist3@gmail.com", InstagramUrl = "#artist3", Phone = "+380978123123", Songs = new List<Song> { songs[4], songs[5] } },
                new Artist { Name = "Artist 4", DateOfBirth = new DateTime(2004, 12, 4), Email = "Artist4@gmail.com", InstagramUrl = "#artist4", Phone = "+380978123123", Songs = new List<Song> { songs[6], songs[7] } },
                new Artist { Name = "Artist 5", DateOfBirth = new DateTime(2005, 12, 5), Email = "Artist5@gmail.com", InstagramUrl = "#artist5", Phone = "+380978123123", Songs = new List<Song> { songs[1], songs[8], songs[9] } }
            };
            if (!_context.Artists.Any() && !_context.Genres.Any() && !_context.Songs.Any())
            {
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await _context.Genres.AddRangeAsync(genres);
                        await _context.SaveChangesAsync();
                        await _context.Songs.AddRangeAsync(songs);
                        await _context.SaveChangesAsync();
                        await _context.Artists.AddRangeAsync(artists);
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
}
