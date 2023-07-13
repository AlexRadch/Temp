using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace GC_AllocateNewArray_Bench;

[MemoryDiagnoser]
public class BenchArrayString
{
    //[Params(0, 1, 5)]
    //[Params(0, 1, 5, 10, 100, 1_000)]
    //[Params(0, 1, 5, 10, 100, 1_000, 10_000)]
    //[Params(0, 1, 5, 10, 100, 1_000, 10_000, 100_000)]
    [Params(0, 1, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    //[Params(0, 1, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000, 10_000_000)]
    public int Count { get; set; }

    [Benchmark(Baseline = true)]
    public void Create()
    {
        _ = new string[Count];
    }

    [Benchmark]
    public void Allocate()
    {
        _ = GC.AllocateArray<string>(Count);
    }

    [Benchmark]
    public void AllocateUninitialized()
    {
        _ = GC.AllocateUninitializedArray<string>(Count);
    }
}
