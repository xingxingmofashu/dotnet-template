using XBoot.Composables;
using XBoot.Core.Model;


namespace XBoot.Playground.Services
{
    public interface IDemoService
    {
        Task<XBootPageResponse<Users>> GetUsersAsync(int pageIndex, int pageCount);
    }
}
