using Bogus;
using Book.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Book.Server.Models
{
    public class DataGenerator
    {
        public static void Initialize(IDbContextFactory<AppDbContext> appDbContextFactory)
        {
            List<Book.Shared.Models.Book> people = new List<Book.Shared.Models.Book>();
            Randomizer.Seed = new Random(32321);
            var appDbContext = appDbContextFactory.CreateDbContext();
            if (!appDbContext.Users.Any())
            {
                var testUsers = new Faker<User>()
                    .RuleFor(u => u.FirstName, u => u.Name.FirstName())
                    .RuleFor(u => u.LastName, u => u.Name.LastName())
                    .RuleFor(u => u.Username, u => u.Internet.UserName())
                    .RuleFor(u => u.Password, u => u.Internet.Password());
                var users = testUsers.Generate(4);

                User customUser = new User()
                {
                    FirstName = "Taufiq",
                    LastName = "Smith",
                    Username = "admin",
                    Password = "admin"
                };

                users.Add(customUser);

                foreach (User u in users)
                {
                    u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(u.Password);
                    u.Password = "**********";
                    appDbContext.Users.Add(u);
                }
                appDbContext.SaveChanges();


            }

        }
        private static Random random = new Random();

        static string LoremIpsum(int minWords, int maxWords,
          int minSentences, int maxSentences,
          int numParagraphs)
        {

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
        "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
        "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences)
                + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            StringBuilder result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result.Append(" "); }
                        result.Append(words[rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
            }

            return result.ToString();
        }
    }
}