using BenchmarkDotNet.Attributes;

namespace Guid_BigLittleEndian_Bench;

[MemoryDiagnoser]
public class BenchTryWriteBytes
{

    private static readonly byte[] fromBytes = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF };
    private static readonly GuidO guidO = new GuidO(fromBytes);
    private static readonly GuidN guidN = new GuidN(fromBytes);

    private byte[] toBytes = new byte[16];

    [Benchmark(Baseline = true)]
    public void GO_TryWriteBytes()
    {
        guidO.TryWriteBytes(toBytes);
        guidO.TryWriteBytes(toBytes);
        guidO.TryWriteBytes(toBytes);
        guidO.TryWriteBytes(toBytes);
        guidO.TryWriteBytes(toBytes);

        guidO.TryWriteBytes(toBytes);
        guidO.TryWriteBytes(toBytes);
        guidO.TryWriteBytes(toBytes);
        guidO.TryWriteBytes(toBytes);
        guidO.TryWriteBytes(toBytes);
    }

    [Benchmark]
    public void GN_TryWriteBytes()
    {
        guidN.TryWriteBytes(toBytes);
        guidN.TryWriteBytes(toBytes);
        guidN.TryWriteBytes(toBytes);
        guidN.TryWriteBytes(toBytes);
        guidN.TryWriteBytes(toBytes);

        guidN.TryWriteBytes(toBytes);
        guidN.TryWriteBytes(toBytes);
        guidN.TryWriteBytes(toBytes);
        guidN.TryWriteBytes(toBytes);
        guidN.TryWriteBytes(toBytes);
    }
}
