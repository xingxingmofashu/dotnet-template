using XBoot.Composables;

namespace XBoot.Playground.IService
{
    public interface IDemoService
    {
        Task<XBootResponse> Get();
    }
}
