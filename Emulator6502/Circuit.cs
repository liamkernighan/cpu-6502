namespace Emulator6502;

public class Circuit
{
    private readonly Cpu _cpu;
    private readonly Memory _memory;

    public Circuit(Cpu cpu, Memory memory)
    {
        _cpu = cpu;
        _memory = memory;
    }

    public void Execute()
    {
        const byte firstAddress = 0x01;

        // // Кладём инструкцию
        // _memory[0xFFFC] = Cpu.INS_LDA_ZP;
        // // Кладём адрес
        // _memory[0xFFFD] = firstAddress;

        // Кладём значение по адресу, которое должно оказаться в регистре A.
        // _memory[firstAddress] = 0xFE;

        _memory.Write16(0xFFFC, firstAddress);

        // First clock. Load 0xDD into A
        _memory[0x1] = Cpu.LDA_IMMEDIATE;
        _memory[0x2] = 0xA0;

        // Second clock. Load value at address on page zero
        _memory[0x3] = Cpu.LDA_ZERO_PAGE;
        _memory[0x4] = 0x10;
        _memory[0x10] = 0xA1;

        // должен получиться 0xFF00;
        _cpu.Reset();
        _cpu.Clock();

        Assert(_cpu.A == 0xA0);

        _cpu.Clock();
        Assert(_cpu.A == 0xA1);

        _memory[0x5] = Cpu.LDX_IMMEDIATE;
        _memory[0x6] = 0x2;
        _cpu.Clock();
        Assert(_cpu.A == 0xA1);
        Assert(_cpu.X == 0x2);

        _memory[0x7] = Cpu.LDA_ZERO_PAGE_VALUE_PLUS_X;
        _memory[0x8] = 0x1;
        // Получаем 0x3, таким образом в A должно лежать 0xA5 (LDA_ZERO_PAGE)
        _cpu.Clock();
        Assert(_cpu.A == Cpu.LDA_ZERO_PAGE);
        Assert(_cpu.X == 0x2);


        // _cpu.Reset();
        //
        //
        // _cpu.Clock();
        // Assert(_cpu.A == 0xFE);


    }

    private void Assert(bool value)
    {
        if (!value)
            throw new InvalidOperationException();
    }
}