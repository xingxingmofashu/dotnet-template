using Microsoft.AspNetCore.Mvc;
using XBoot.Playground.IService;

namespace XBoot.Playground.Controllers
{
    [ApiExplorerSettings(GroupName = "Playground")]
    [ApiController]
    [Route("api/playground/[controller]")]
    public class DemoController: ControllerBase
    {
        private readonly IDemoService _demoService;

        public DemoController(IDemoService demoService)
        {
            _demoService = demoService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _demoService.Get());
        }
    }
}
