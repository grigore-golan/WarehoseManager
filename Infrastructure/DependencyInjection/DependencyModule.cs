using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public abstract class DependencyModule
{
    public abstract void Load(IServiceCollection services, IConfiguration configuration);
}
