using XBoot.Composables;

namespace XBoot.Playground.Services
{
    public interface IDemoService
    {
        Task<XBootResponse> Get();
    }
}
