using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace StartupTweet
{
    public class TwitterApi
    {
        public IEnumerable<ITweet> GetTweets()
        {
            // Set up your credentials (https://apps.twitter.com)
            Auth.SetUserCredentials("B1QeRBJ78iqcuRdkPhxeDe19r", "8yGJufH2ujGieDC1qHQ2WZmEA3gPHvCYtrOJRUuvyfObNXBi3q", "2387675090-2gq4MP0IQC04okCALMgsJmLbgxdVZ7abs2zH25L", "CCmBFpXcX14lMrmzJMJYzsDntOfPAS1jijVpUHvdC1bes");
            var searchParameter = new SearchTweetsParameters("startup")
            {
                MaximumNumberOfResults = 30,
            };

            return Search.SearchTweets(searchParameter);

        }
    }
}
