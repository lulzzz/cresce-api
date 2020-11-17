using System.Linq;
using System.Threading.Tasks;
using Cresce.Core.Organizations;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Organization
{
    [ApiController]
    [Route("api/v1")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _service;

        public OrganizationController(IOrganizationService service)
        {
            _service = service;
        }

        [HttpGet("{userId}/[controller]")]
        public async Task<IActionResult> Index(string userId)
        {
            var organizations = await _service.GetOrganizations(userId);
            if (organizations.Any())
            {
                return Ok(organizations);
            }

            return NotFound();
        }
    }
}
