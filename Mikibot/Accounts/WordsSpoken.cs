namespace Miki.Accounts
{
    public class WordsSpoken
    {
        private string[] FilterWords;
        public int MessagesSent;

        public void Initialize()
        {
            FilterWords = GetWordsFromFile();
        }

        private string[] GetWordsFromFile()
        {
            return null;
        }
    }
}