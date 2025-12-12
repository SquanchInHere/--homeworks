using BenchmarkDotNet.Attributes;
using System.Linq;
using Microsoft.VSDiagnostics;

namespace AverageScore.Benchmarks
{
    [CPUUsageDiagnoser]
    public class AverageBenchmarks
    {
        private double[] data;
        [GlobalSetup]
        public void Setup()
        {
            var rnd = new System.Random(42);
            data = Enumerable.Range(0, 1000).Select(_ => rnd.NextDouble() * 5).ToArray();
        }

        [Benchmark(Baseline = true)]
        public double LoopAverage()
        {
            double sum = 0;
            for (int i = 0; i < data.Length; i++)
                sum += data[i];
            return sum / data.Length;
        }

        [Benchmark]
        public double LinqAverage()
        {
            return data.Average();
        }
    }
}