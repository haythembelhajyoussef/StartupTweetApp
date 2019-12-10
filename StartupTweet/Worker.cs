using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StartupTweet.Models;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace StartupTweet
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Auth.SetUserCredentials("B1QeRBJ78iqcuRdkPhxeDe19r", "8yGJufH2ujGieDC1qHQ2WZmEA3gPHvCYtrOJRUuvyfObNXBi3q", "2387675090-2gq4MP0IQC04okCALMgsJmLbgxdVZ7abs2zH25L", "CCmBFpXcX14lMrmzJMJYzsDntOfPAS1jijVpUHvdC1bes");
            Database db = new Database("Twitter");
            var tweetsFromDB = db.LoadRecords<Models.Tweet>("Tweets");

            var searchParameter = new SearchTweetsParameters("startup")
            {
                MaximumNumberOfResults = 30,
            };


            while (!stoppingToken.IsCancellationRequested)
            {
                int tweetOccur = 0;


                var tweetsFromTwitter = Search.SearchTweets(searchParameter);
                foreach (var tweet in tweetsFromTwitter)
                {
                    if (!tweetsFromDB.Any(tweetDB => tweetDB.text == tweet.FullText))
                    {
                        db.InsertRecord("Tweets", new Models.Tweet { text = tweet.FullText });
                        tweetsFromDB.Add(new Models.Tweet { text = tweet.FullText });
                        tweetOccur++;
                        //Console.WriteLine("exist");
                    }
                }

                _logger.LogInformation("{tweetOccur} document(s) was added to Database", tweetOccur);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
