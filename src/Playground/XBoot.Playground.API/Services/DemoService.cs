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

    public async Task<XBootResponse> Get()
    {
        return XBootResponse.Success(await _userService.GetAsync(x => x.Account == "administrator"));
    }
}

