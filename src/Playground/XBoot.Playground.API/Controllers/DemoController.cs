using Microsoft.AspNetCore.Mvc;
using XBoot.Playground.Services;

namespace XBoot.Playground.API.Controllers
{
    [ApiExplorerSettings(GroupName = "Playground")]
    [ApiController]
    [Route("api/playground/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(int pageIndex = 1, int pageCount = 10)
        {
            return Ok(await _demoService.GetUsersAsync(pageIndex, pageCount));
        }
    }
}
