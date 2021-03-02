using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _ps;
        private readonly KeepsService _ks;
        private readonly VaultsService _vs;
        public AccountController(ProfilesService ps, KeepsService ks, VaultsService vs)
        {
            _ps = ps;
            _ks = ks;
            _vs = vs;
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("keeps")]
        public async Task<ActionResult<Keep>> GetKeepsByAccountId()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                string id = userInfo.Id;
                IEnumerable<Keep> keeps = _ks.GetByCreatorId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("vaults")]
        public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByAccountId()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                string id = userInfo.Id;
                string userId = userInfo.Id;
                IEnumerable<Vault> vaults = _vs.GetByCreatorId(id, userId);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}