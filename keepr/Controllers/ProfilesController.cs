using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
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
        private readonly KeepsService _ks;
        private readonly VaultsService _vs;
        public ProfilesController(ProfilesService serv, KeepsService ks, VaultsService vs)
        {
            _serv = serv;
            _ks = ks;
            _vs = vs;
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
        [HttpGet("{id}/keeps")]
        public ActionResult<Keep> GetKeepsByUserId(string id)
        {
            try
            {
                IEnumerable<Keep> keeps = _ks.GetByCreatorId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/vaults")]
        public async Task<ActionResult<Vault>> GetVaultsByProfileId(string id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                if (userInfo == null)
                {
                    string userId = "nope";
                    IEnumerable<Vault> vaults = _vs.GetByCreatorId(id, userId);
                    return Ok(vaults);
                }
                else
                {
                    string userId = userInfo.Id;
                    IEnumerable<Vault> vaults = _vs.GetByCreatorId(id, userId);
                    return Ok(vaults);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}