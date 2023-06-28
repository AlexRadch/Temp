using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Guid_BigLittleEndian_Bench;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.Declared)]
public class BenchFromSpan
{

    private static readonly byte[] fromBytes = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF };

    [Benchmark(Baseline = true)]
    public void GO_FromSpan()
    {
        new GuidO(fromBytes);
        new GuidO(fromBytes);
        new GuidO(fromBytes);
        new GuidO(fromBytes);
        new GuidO(fromBytes);

        new GuidO(fromBytes);
        new GuidO(fromBytes);
        new GuidO(fromBytes);
        new GuidO(fromBytes);
        new GuidO(fromBytes);
    }

    [Benchmark]
    public void GN_FromSpan()
    {
        new GuidN(fromBytes);
        new GuidN(fromBytes);
        new GuidN(fromBytes);
        new GuidN(fromBytes);
        new GuidN(fromBytes);

        new GuidN(fromBytes);
        new GuidN(fromBytes);
        new GuidN(fromBytes);
        new GuidN(fromBytes);
        new GuidN(fromBytes);
    }
}
