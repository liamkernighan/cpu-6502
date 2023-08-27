namespace Emulator6502;

public class Cpu
{
    /// <summary>
    /// LDA Immediate
    /// </summary>
    public const byte LDA_IMMEDIATE = 0xA9;

    /// <summary>
    /// LDA Zero page
    /// </summary>
    public const byte LDA_ZERO_PAGE = 0xA5;

    /// <summary>
    /// Берём следующий байт плюсуем к нему значение из регистра X, складываем (с переполнением)
    /// по полученному адресу ставим значение в регистр А.
    /// </summary>
    public const byte LDA_ZERO_PAGE_VALUE_PLUS_X = 0xB5;

    /// <summary>
    /// LDX Immediate
    /// </summary>
    public const byte LDX_IMMEDIATE = 0xA2;

    private readonly Memory _memory;

    public Cpu(Memory memory)
    {
        _memory = memory;
    }

    /// <summary>
    /// Program counter.
    /// References to the location in memory which is currently being executed
    /// </summary>
    public ushort PC;

    /// <summary>
    /// Stack pointer
    /// </summary>
    public byte SP;

    /// <summary>
    /// Accumulator register
    /// </summary>
    public byte A;

    /// <summary>
    /// X Register
    /// </summary>
    public byte X;

    /// <summary>
    /// Y Register
    /// </summary>
    public byte Y;

    public readonly CpuStatusFlags Flags = new();

    public int Cycles = 0;

    public void Reset()
    {
        Flags.Reset();
        PC = _memory.Read16(0xFFFC);

        SP = 0xFD;
        A = X = Y = 0;
    }

    public void Clock()
    {
        byte opCode = FetchByte();

        if (opCode == LDA_IMMEDIATE)
        {
            byte value = FetchByte();
            A = value;
            UpdateFlags();
        }
        else if (opCode == LDA_ZERO_PAGE)
        {
            byte zeroPageAddress = FetchByte();
            A = PeekByte(zeroPageAddress);
            UpdateFlags();
        }
        else if (opCode == LDA_ZERO_PAGE_VALUE_PLUS_X)
        {
            byte zeroPageAddress = FetchByte();
            // byte + byte = int, so we need to overflow
            byte resultAddress = (byte) ((zeroPageAddress + X) & 0xFF);
            Cycles--;
            A = PeekByte(resultAddress);
            UpdateFlags();
        }
        else if (opCode == LDX_IMMEDIATE)
        {
            byte value = FetchByte();
            X = value;
            UpdateFlags();
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    private void UpdateFlags()
    {
        Flags.Z = A == 0;
        Flags.N = (A & 0b10000000) > 0;
    }

    private byte FetchByte()
    {
        byte result = _memory[PC];
        PC++;
        Cycles--;
        return result;
    }

    private byte PeekByte(byte address)
    {
        byte result = _memory[address];
        Cycles--;
        return result;
    }
}