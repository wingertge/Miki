using DiscordSharp.Events;
using DiscordSharp.Objects;
using IMDBNet;
using Miki.Core;
using Miki.Core.Command;
using System.Threading;

namespace Miki.Extensions.IMDb
{
    internal class IMDbNet_Discord : Command
    {
        public string movieTitle;
        public DiscordChannel channel;

        public override void Initialize()
        {
            id = "imdb";
            appearInHelp = true;
            parameterType = ParameterType.YES;
            description = "get movie data based on the title you add";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            base.PlayCommand(e);
            IMDbNet_Discord imdb = new IMDbNet_Discord();
            imdb.channel = e.Channel;
            string messageTrimmed = message.Substring(5);
            imdb.movieTitle = messageTrimmed;
            Thread t = new Thread(new ThreadStart(imdb.GetData), 0);
            t.Start();
            return;
        }

        public void GetData()
        {
            IMDBMovie movie = IMDB.GetMovie(movieTitle);
            channel.SendMessage("https://www.imdb.com/title/" + movie.imdbID + '\n' + "title: " + movie.title + '\n' + "plot: " + movie.plot + '\n' + "rating: " + movie.imdbRating + "/10");
        }
    }
}