using System.Diagnostics;

namespace AdventOfCode.Tests
{
    internal static class Extensions
    {
        internal static void RunAndValidateExecutionTime(Action test)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            test.Invoke();
            stopwatch.Stop();
            Assert.True(stopwatch.ElapsedMilliseconds <= Constants.TestTimeoutMs, Constants.TimeoutExceededMessage);
        }
    }
}
