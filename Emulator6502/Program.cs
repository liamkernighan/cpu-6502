
using Emulator6502;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    public static void Main(string[] args)
    {
        using var host = args.CreateHost();
        var circuit = host.Services.GetRequiredService<Circuit>();
        circuit.Execute();
    }
}