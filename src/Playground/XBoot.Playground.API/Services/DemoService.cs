using XBoot.Composables;
using XBoot.Core.IServices;
using XBoot.Core.Model;
using XBoot.Playground.Services;

namespace XBoot.Playground.API.Services;

public class DemoService : IDemoService
{
    private readonly IRepository<Users> _userService;

    public DemoService(IRepository<Users> userService)
    {
        _userService = userService;
    }

    public async Task<XBootPageResponse<Users>> GetUsersAsync(int pageIndex, int pageCount)
    {
        var response= await _userService.QueryPageAsync<Users>(x => !x.IsDeleted, pageIndex, pageCount);
        return new XBootPageResponse<Users>(){
            Data=response.T,
            PageIndex=response.pageIndex,
            PageSize=response.pageSize,
            TotalCount=response.totalCount
        };
    }
}

