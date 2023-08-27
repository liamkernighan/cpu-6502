using System.Runtime.InteropServices;

namespace Emulator6502;

public unsafe class Circuit : IDisposable
{
    private readonly Cpu* _cpu;
    private readonly Memory _memory = new Memory();

    public Circuit()
    {
        _cpu = (Cpu*)Marshal.AllocHGlobal(sizeof(Cpu));
    }

    public void Execute(int clockCycles)
    {
        _cpu->A = 0xFF;
        _cpu->Flags.D = 0xFF;
    }

    public void Dispose()
    {
        Marshal.FreeHGlobal((IntPtr)_cpu);
    }
}