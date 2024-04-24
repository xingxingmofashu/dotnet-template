using XBoot.Composables;
using XBoot.Playground.Services;

namespace XBoot.Playground.API.Service;

public class DemoService : IDemoService
{
    public async Task<XBootResponse> Get()
    {
        //await _userService.GetAsync(x => x.Account == "administrator")
        return XBootResponse.Success(new
        {
            Account = "administrator"
        });
    }
}

