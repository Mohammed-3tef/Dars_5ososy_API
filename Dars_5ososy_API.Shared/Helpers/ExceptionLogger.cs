namespace Dars_5ososy_API.Shared.Helpers
{
    public class ExceptionLogger
    {
        public static async Task Log(Exception message)
        {
            string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Logs");
            Directory.CreateDirectory(logDirectory);

            string fileName = $"Log_{DateTime.Now:yyyy-MM-dd}.txt";
            string path = Path.Combine(logDirectory, fileName);

            using (var stream = new StreamWriter(path, append: true))
            {
                //await stream.WriteLineAsync(message.Message);
                await stream.WriteLineAsync($"[{DateTime.Now:HH:mm:ss}] {message}");
            }
        }
    }
}
