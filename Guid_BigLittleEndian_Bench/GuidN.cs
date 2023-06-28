using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Guid_BigLittleEndian_Bench;

// Represents a Globally Unique Identifier.
[StructLayout(LayoutKind.Sequential)]
[Serializable]
public readonly struct GuidN
{
    private readonly int _a;   // Do not rename (binary serialization)
    private readonly short _b; // Do not rename (binary serialization)
    private readonly short _c; // Do not rename (binary serialization)
    private readonly byte _d;  // Do not rename (binary serialization)
    private readonly byte _e;  // Do not rename (binary serialization)
    private readonly byte _f;  // Do not rename (binary serialization)
    private readonly byte _g;  // Do not rename (binary serialization)
    private readonly byte _h;  // Do not rename (binary serialization)
    private readonly byte _i;  // Do not rename (binary serialization)
    private readonly byte _j;  // Do not rename (binary serialization)
    private readonly byte _k;  // Do not rename (binary serialization)

    public GuidN(ReadOnlySpan<byte> b)
    {
        if (b.Length != 16)
        {
            ThrowGuidArrayCtorArgumentException();
        }

        this = MemoryMarshal.Read<GuidN>(b);

        if (!BitConverter.IsLittleEndian)
        {
            _a = BinaryPrimitives.ReverseEndianness(_a);
            _b = BinaryPrimitives.ReverseEndianness(_b);
            _c = BinaryPrimitives.ReverseEndianness(_c);
        }
    }

    [DoesNotReturn]
    [StackTraceHidden]
    private static void ThrowGuidArrayCtorArgumentException()
    {
        throw new ArgumentException("b");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe ReadOnlySpan<byte> AsBytes(in GuidN source) =>
        new ReadOnlySpan<byte>(Unsafe.AsPointer(ref Unsafe.AsRef(in source)), sizeof(Guid));

    // Returns an unsigned byte array containing the GUID.
    public byte[] ToByteArray()
    {
        var g = new byte[16];
        if (!BitConverter.IsLittleEndian)
        {
            MemoryMarshal.TryWrite(g, ref Unsafe.AsRef(in this));
        }
        else
        {
            // slower path for BigEndian
            GuidN guid = new GuidN(AsBytes(this));
            MemoryMarshal.TryWrite(g, ref Unsafe.AsRef(in guid));
        }
        return g;
    }

    public bool TryWriteBytes(Span<byte> destination)
    {
        if (destination.Length < 16)
            return false;

        if (!BitConverter.IsLittleEndian)
        {
            MemoryMarshal.TryWrite(destination, ref Unsafe.AsRef(in this));
        }
        else
        {
            // slower path for BigEndian
            GuidN guid = new GuidN(AsBytes(this));
            MemoryMarshal.TryWrite(destination, ref Unsafe.AsRef(in guid));
        }
        return true;
    }

}
