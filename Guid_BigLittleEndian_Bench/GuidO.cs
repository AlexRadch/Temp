using System.Buffers.Binary;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Guid_BigLittleEndian_Bench;

// Represents a Globally Unique Identifier.
[StructLayout(LayoutKind.Sequential)]
[Serializable]
public readonly struct GuidO
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

    public GuidO(ReadOnlySpan<byte> b)
    {
        if (b.Length != 16)
        {
            ThrowArgumentException();
        }

        if (!BitConverter.IsLittleEndian)
        {
            this = MemoryMarshal.Read<GuidO>(b);
            return;
        }

        // slower path for BigEndian:
        _k = b[15];  // hoist bounds checks
        _a = BinaryPrimitives.ReadInt32LittleEndian(b);
        _b = BinaryPrimitives.ReadInt16LittleEndian(b.Slice(4));
        _c = BinaryPrimitives.ReadInt16LittleEndian(b.Slice(6));
        _d = b[8];
        _e = b[9];
        _f = b[10];
        _g = b[11];
        _h = b[12];
        _i = b[13];
        _j = b[14];

        [DoesNotReturn]
        [StackTraceHidden]
        static void ThrowArgumentException()
        {
            throw new ArgumentException(nameof(b));
        }
    }

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
            TryWriteBytes(g);
        }
        return g;
    }

    public bool TryWriteBytes(Span<byte> destination)
    {
        if (!BitConverter.IsLittleEndian)
        {
            return MemoryMarshal.TryWrite(destination, ref Unsafe.AsRef(in this));
        }

        // slower path for BigEndian
        if (destination.Length < 16)
            return false;

        destination[15] = _k; // hoist bounds checks
        BinaryPrimitives.WriteInt32LittleEndian(destination, _a);
        BinaryPrimitives.WriteInt16LittleEndian(destination.Slice(4), _b);
        BinaryPrimitives.WriteInt16LittleEndian(destination.Slice(6), _c);
        destination[8] = _d;
        destination[9] = _e;
        destination[10] = _f;
        destination[11] = _g;
        destination[12] = _h;
        destination[13] = _i;
        destination[14] = _j;
        return true;
    }
}
