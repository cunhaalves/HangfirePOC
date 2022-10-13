using Newtonsoft.Json.Linq;

namespace HangfirePOC
{
    public class Work
    {
        public async Task DoWork()
        {
            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
