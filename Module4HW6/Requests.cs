using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module4HW6.Entities;

namespace Module4HW6
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
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SaveChangesAsync();
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
