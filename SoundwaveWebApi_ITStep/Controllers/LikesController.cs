using Core.Dtos;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SoundwaveWebApi_ITStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{Roles.ADMIN},{Roles.PROUSER},{Roles.USER}", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService likeService;

        public LikesController(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await likeService.GetAll());
        }

        [HttpGet("likedByUser")]
        public async Task<IActionResult> GetAllUserId(string userId)
        {
            return Ok(await likeService.GetLikedTracksByUserId(userId));
        }

        [HttpGet("getLike")]
        public async Task<IActionResult> Get(int id)
        { 
            return Ok(await likeService.GetLike(id));
        }

        [HttpPost("addLike")]
        public async Task<IActionResult> AddLike([FromForm] CreateLikeDto model)
        {
            await likeService.AddLike(model);
            return Ok();
        }
        
        [HttpPost("removeLike")]
        public async Task<IActionResult> RemoveLike(int id)
        {
            await likeService.RemoveLike(id);
            return Ok();
        }
    }
}
