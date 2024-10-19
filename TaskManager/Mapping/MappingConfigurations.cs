using Mapster;
using TaskManager.Contracts.Tasks;

namespace TaskManager.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Entities.Task, TaskResponse>().Map(dest => dest.Status, src => src.IsDone);
    }
}
