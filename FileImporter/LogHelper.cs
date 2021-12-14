namespace FileImporter
{
    using System;

    public class LogHelper
    {
        public void LogProgression(ref int i, int length, int step)
        {
            if (++i % step == 0)
            {
                Console.Out.WriteLineAsync($"{i}/{length} {(i * 100 / length):D}%");
            }
        }
        public void LogProgression(ref int i, long length, long fileLength, int step)
        {
            if (++i % step == 0)
            {
                Console.Out.WriteLineAsync($"{i} {(length * 100 / fileLength):D}%");
            }
        }
    }
}
