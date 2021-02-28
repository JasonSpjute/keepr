using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Party_Planner.Exceptions;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _serv;
        public VaultKeepsController(VaultKeepsService serv)
        {
            _serv = serv;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vk)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                vk.CreatorId = userInfo.Id;
                return Ok(_serv.Create(vk));

            }
            catch (NotAuthorized e)
            {
                return Forbid(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                string userId = userInfo.Id;
                _serv.Delete(id, userId);
                return Ok("This Keep has been Removed from your Vault");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}