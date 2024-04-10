using System.Diagnostics;

namespace TCWPlace.Tests
{
    public class StressTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("https://localhost:7252")]
        [TestCase("http://localhost:3000")]
        public async Task Get(string endpoint)
        {
            var testDuration = TimeSpan.FromSeconds(10);

            var httpClient = new HttpClient();

            var stopwatch = new Stopwatch();

            var requestDurations = new List<TimeSpan>(10_000_000);

            var endTime = DateTime.UtcNow + testDuration;

            while (DateTime.UtcNow < endTime)
            {
                stopwatch.Start();
                await httpClient.GetAsync($"{endpoint}/get");
                stopwatch.Stop();

                requestDurations.Add(stopwatch.Elapsed);
                stopwatch.Reset();
            }

            await Console.Out.WriteLineAsync($"Endpoint: {endpoint}");
            await Console.Out.WriteLineAsync($"Test duration: {testDuration.TotalSeconds} s");
            await Console.Out.WriteLineAsync($"Number of requests: {requestDurations.Count}");
            await Console.Out.WriteLineAsync($"Avg request duration: {requestDurations.Average(d => d.TotalMilliseconds)} ms");
            await Console.Out.WriteLineAsync($"Requests per second: {requestDurations.Count / testDuration.TotalSeconds}");
        }

        [TestCase("https://localhost:7252")]
        [TestCase("http://localhost:3000")]
        public async Task Change(string endpoint)
        {
            var httpClient = new HttpClient();

            var stopwatch = new Stopwatch();

            var requestDurations = new List<TimeSpan>(320 * 180);

            var totalDurationWatch = new Stopwatch();
            totalDurationWatch.Start();

            for (var x = 0; x < 320; x++)
            {
                for (var y = 0; y < 180; y++)
                {
                    stopwatch.Start();
                    await httpClient.GetAsync($"{endpoint}/change?x={x}&y={y}&col=abc123");
                    stopwatch.Stop();

                    requestDurations.Add(stopwatch.Elapsed);
                    stopwatch.Reset();
                }
            }

            totalDurationWatch.Stop();

            await Console.Out.WriteLineAsync($"Endpoint: {endpoint}");
            await Console.Out.WriteLineAsync($"Test duration: {totalDurationWatch.Elapsed.TotalSeconds} s");
            await Console.Out.WriteLineAsync($"Number of requests: {requestDurations.Count}");
            await Console.Out.WriteLineAsync($"Avg request duration: {requestDurations.Average(d => d.TotalMilliseconds)} ms");
            await Console.Out.WriteLineAsync($"Requests per second: {requestDurations.Count / totalDurationWatch.Elapsed.TotalSeconds}");
        }
    }
}