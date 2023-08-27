using System.Runtime.CompilerServices;

namespace Emulator6502;

public static class StructExtensions
{
    public static unsafe byte[] SerializeValueType<T>(in T value) where T : unmanaged
    {
        byte[] result = new byte[sizeof(T)];
        Unsafe.As<byte, T>(ref result[0]) = value;
        return result;
    }

    // Note: Validation is omitted for simplicity
    public static T DeserializeValueType<T>(this byte[] data) where T : unmanaged
        => Unsafe.As<byte, T>(ref data[0]);
}