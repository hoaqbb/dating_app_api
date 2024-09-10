using AutoMapper;
using datingapp_api.Data.Entities;
using datingapp_api.DTOs;
using datingapp_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace datingapp_api.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }
        //P@ssWord123

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetMemberByUsername(string username)
        {
            var user = await _userRepository.GetMemberByUsernameAsync(username);
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);

            _userRepository.Update(user);

            if(await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user!");
        }

        //[HttpGet("server-error")]
        //public ActionResult<string> GetServerError()
        //{
        //    var thing = _context.Users.Find(-1);

        //    var thingToReturn = thing.ToString();

        //    return thingToReturn;
        //}
    }
}
