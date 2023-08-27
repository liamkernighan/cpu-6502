using System.Text;

namespace Emulator6502;

public class Memory
{
    private byte[] _buffer = new byte[ushort.MaxValue + 1];

    public byte this[ushort address]
    {
        get => _buffer[address];
        set => _buffer[address] = value;
    }

    /// <summary>
    /// Reads two bytes from the specified address (little endian).
    /// </summary>
    /// <returns>A 16 bit value representing the two bytes read in a little endian fashion.</returns>
    /// <param name="address">The address to read two bytes from</param>
    public ushort Read16(ushort address)
    {
        byte lo = this[address];
        byte hi = this[(ushort)(address + 1)];
        return (ushort)((hi << 8) | lo);
    }

    public void Write16(ushort address, ushort value)
    {
        byte hi = (byte) (value >> 8);
        var lo = (byte) (value & 0x00FF);

        this[address] = lo;
        this[(ushort) (address + 1)] = hi;
    }

    public void Reset()
    {
        Array.Clear(_buffer);
    }

    public string Dump()
    {
        const int dim = 256;
        var sb = new StringBuilder();
        for (int row = 0; row < dim; row++)
        {
            for (int col = 0; col < dim; col++)
            {
                int index = dim * row + col;
                var value = this[(ushort)index].ToString("X2");
                sb.Append(value + " ");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}