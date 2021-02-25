public class Parser
{
    /// <summary>
    /// Parse the value from the given string.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static float Parse(string s)
    {
        var result = -1f;
        s = s.Trim();
        //s = "[RUN #1 3%,  19 secs]  1 threads:     4828197 ops,  256178 (avg:  254044) ops/sec, 145.87MB/sec (avg: 145.65MB/sec),  0.85 (avg:  0.86) msec latency\n" +
        //    "[RUN #1 3%,  19 secs]  1 threads:     4828197 ops,  256178 (avg:  254044) ops/sec, 145.87MB/sec (avg: 145.65MB/sec),  0.85 (avg:  0.47) msec latency\n" +
        //    "[RUN #1 3%,  19 secs]  1 threads:     4828197 ops,  256178 (avg:  254044) ops/sec, 145.87MB/sec (avg: 145.65MB/sec),  0.85 (avg:  0.23) msec latency\n" +
        //    "[RUN #1 3%,  19 secs]  1 threads:     4828197 ops,  256178 (avg:  254044) ops/sec, 145.87MB/sec (avg: 145.65MB/sec),  0.85 (avg:  0.15) msec latency\n";

        try
        {
            //var lines = s.Trim().Split('\n');
            //var commaDemlimitedColumns = lines[lines.Length - 1].Split(' ');
            //var msValue = commaDemlimitedColumns[commaDemlimitedColumns.Length - 3].Replace(")", string.Empty);
            //result = float.Parse(msValue);
            result = float.Parse(s);
        }
        catch
        {
            result = -1f;
        }

        return result;
    }
}
