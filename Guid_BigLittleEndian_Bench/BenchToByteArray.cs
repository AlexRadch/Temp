using BenchmarkDotNet.Attributes;

namespace Guid_BigLittleEndian_Bench;

[MemoryDiagnoser]
public class BenchToByteArray
{

    private static readonly byte[] fromBytes = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF };
    private static readonly GuidO guidO = new GuidO(fromBytes);
    private static readonly GuidN guidN = new GuidN(fromBytes);

    [Benchmark(Baseline = true)]
    public void GO_ToByteArray()
    {
        _ = guidO.ToByteArray();
        _ = guidO.ToByteArray();
        _ = guidO.ToByteArray();
        _ = guidO.ToByteArray();
        _ = guidO.ToByteArray();

        _ = guidO.ToByteArray();
        _ = guidO.ToByteArray();
        _ = guidO.ToByteArray();
        _ = guidO.ToByteArray();
        _ = guidO.ToByteArray();
    }

    [Benchmark]
    public void GN_FromSpan()
    {
        _ = guidN.ToByteArray();
        _ = guidN.ToByteArray();
        _ = guidN.ToByteArray();
        _ = guidN.ToByteArray();
        _ = guidN.ToByteArray();

        _ = guidN.ToByteArray();
        _ = guidN.ToByteArray();
        _ = guidN.ToByteArray();
        _ = guidN.ToByteArray();
        _ = guidN.ToByteArray();
    }
}
