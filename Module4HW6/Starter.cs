using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module4HW6.Helpers;

namespace Module4HW6
{
    public class Starter
    {
        public void Run(string[] args)
        {
            using (var db = new SampleContextFactory().CreateDbContext(args))
            {
                var initializeDb = new InitializeDb(db);
                initializeDb.AddTestData().GetAwaiter().GetResult();
                var requests = new Requests(db);
            }

            System.Console.ReadKey();
        }
    }
}
