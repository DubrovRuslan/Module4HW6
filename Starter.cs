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
                Console.WriteLine("===== Request 1 ======");
                requests.Request01().GetAwaiter().GetResult();
                Console.WriteLine("===== Request 2 ======");
                requests.Request02().GetAwaiter().GetResult();
                Console.WriteLine("===== Request 3 ======");
                requests.Request03().GetAwaiter().GetResult();
            }

            Console.ReadKey();
        }
    }
}