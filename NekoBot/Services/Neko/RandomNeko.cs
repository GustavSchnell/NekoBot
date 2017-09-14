namespace NekoBot.Services.Neko
{
    public class RandomNeko
    {
        public RandomNeko(string file)
        {
            Url = file;
        }

        public string Url { get; private set; }
    }
}
