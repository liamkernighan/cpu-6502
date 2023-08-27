namespace Emulator6502;

static class CpuExtensions
{
    public static unsafe void Reset(this ref Cpu cpu)
    {
        for (int i = 0; i < CpuStatusFlags.LENGTH; i++)
            cpu.Flags.Buffer[i] = 0;

        cpu.PC = 0xFFFC;
        cpu.SP = 0x0100;
        cpu.A = cpu.X = cpu.Y = 0;
    }
}