using DiscordSharp.Objects;
using IMDBNet;

namespace Miki
{
    class IMDbNet_Discord
    {
        public string movieTitle;
        public DiscordChannel channel;

        public void GetData()
        {
            IMDBMovie movie = IMDB.GetMovie(movieTitle);
            channel.SendMessage("https://www.imdb.com/title/" + movie.imdbID + '\n' + "title: " + movie.title + '\n' + "plot: " + movie.plot + '\n' + "rating: " + movie.imdbRating + "/10");
        }
    }
}
