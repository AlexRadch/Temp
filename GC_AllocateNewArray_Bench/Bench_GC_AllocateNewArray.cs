using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace GC_AllocateNewArray_Bench;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.Declared)]
public class BenchFromSpan
{
    //[Params(0, 1, 5)]
    //[Params(0, 1, 5, 10, 100, 1_000)]
    //[Params(0, 1, 5, 10, 100, 1_000, 10_000)]
    //[Params(0, 1, 5, 10, 100, 1_000, 10_000, 100_000)]
    [Params(0, 1, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000)]
    //[Params(0, 1, 5, 10, 100, 1_000, 10_000, 100_000, 1_000_000, 10_000_000)]
    public int Count { get; set; }

    [Benchmark(Baseline = true)]
    public void Array_Int_Create()
    {
        _ = new int[Count];
    }

    [Benchmark]
    public void Array_Int_Allocate()
    {
        _ = GC.AllocateArray<int>(Count);
    }

    [Benchmark]
    public void Array_Int_AllocateUninitialized()
    {
        _ = GC.AllocateUninitializedArray<int>(Count);
    }
}
