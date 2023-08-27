using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Emulator6502;

public static class ServiceCollectionExtensions
{
    public IHost CreateHost(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        var services = builder.Services;
        services.AddSingleton<Cpu>();
        services.AddSingleton<Memory>();
    }
}