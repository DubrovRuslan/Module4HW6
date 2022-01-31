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
            ////var builder = new ConfigurationBuilder();
            ////builder.SetBasePath(Directory.GetCurrentDirectory());
            ////builder.AddJsonFile("Config.json");
            ////var config = builder.Build();
            ////var connectionString = config.GetConnectionString("DefaultConnection");

            ////var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            ////var options = optionsBuilder.
            ////    UseSqlServer(connectionString)
            ////    .Options;
            var factory = new SampleContextFactory();

            using (var db = factory.CreateDbContext(args))
            {
                var initializeDb = new InitializeDb(db);
                initializeDb.AddTestData().GetAwaiter().GetResult();
                var requests = new Requests(db);
            }

            System.Console.ReadKey();
        }
    }
}
