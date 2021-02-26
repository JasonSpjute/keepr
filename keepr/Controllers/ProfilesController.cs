using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _serv;
        public ProfilesController(ProfilesService serv)
        {
            _serv = serv;
        }
        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                Profile profile = _serv.GetProfileById(id);
                return Ok(profile);
            }
            catch (System.Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}