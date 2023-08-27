
using Emulator6502;
using System.Runtime.InteropServices;

class Program
{
    public static void Main(string[] args)
    {
        var circuit = new Circuit();
        circuit.Execute(2);
    }
}


[StructLayout(LayoutKind.Explicit)]
unsafe struct CpuStatusFlags
{

    public fixed byte Buffer[7];
    [FieldOffset(0)]
    public byte C;
    [FieldOffset(1)]
    public byte Z;
    [FieldOffset(2)]
    public byte I;
    [FieldOffset(3)]
    public byte D;
    [FieldOffset(4)]
    public byte B;
    [FieldOffset(5)]
    public byte V;
    [FieldOffset(6)]
    public byte N;
}


struct Cpu
{
    /// <summary>
    /// Program counter
    /// </summary>
    public ushort PC;

    /// <summary>
    /// Stack pointer
    /// </summary>
    public ushort SP;

    /// <summary>
    /// Registers
    /// </summary>
    public byte A, X, Y;

    public CpuStatusFlags Flags;
}

struct Memory
{
    private byte[] _buffer = new byte[ushort.MaxValue + 1];

    public Memory()
    {
    }

    public byte this[ushort address]
    {
        get => _buffer[address];
        set => _buffer[address] = value;
    }

    public void Reset()
    {
        Array.Clear(_buffer);
    }
}
