using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftITOFlix.Data;
using SoftITOFlix.Models;

namespace SoftITOFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftITOFlixUsersController : ControllerBase
    {
        public struct LogInModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        private readonly SignInManager<SoftITOFlixUser> _signInManager;

        public SoftITOFlixUsersController(SignInManager<SoftITOFlixUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: api/SoftITOFlixUsers
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public ActionResult<List<SoftITOFlixUser>> GetUsers(bool includePassive = true)
        {
            IQueryable<SoftITOFlixUser> users = _signInManager.UserManager.Users;
            
            if (includePassive == false)
            {
                users = users.Where(u => u.Passive == false);
            }
            return users.AsNoTracking().ToList();
        }

        // GET: api/SoftITOFlixUsers/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<SoftITOFlixUser> GetSoftITOFlixUser(long id)
        {
            SoftITOFlixUser? softITOFlixUser = null;

            if (User.IsInRole("Administrator") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
                {
                    return Unauthorized();
                }
            }
            softITOFlixUser = _signInManager.UserManager.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefault();

            if (softITOFlixUser == null)
            {
                return NotFound();
            }

            return softITOFlixUser;
        }

        // PUT: api/SoftITOFlixUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize]
        public ActionResult PutSoftITOFlixUser(SoftITOFlixUser softITOFlixUser)
        {
            SoftITOFlixUser? user = null;

            if (User.IsInRole("CustomerRepresentative") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != softITOFlixUser.Id.ToString())
                {
                    return Unauthorized();
                }
            }
            user = _signInManager.UserManager.Users.Where(u => u.Id == softITOFlixUser.Id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }
            user.PhoneNumber = softITOFlixUser.PhoneNumber;
            user.UserName = softITOFlixUser.UserName;
            user.BirthDate = softITOFlixUser.BirthDate;
            user.Email = softITOFlixUser.Email;
            user.Name = softITOFlixUser.Name;
            _signInManager.UserManager.UpdateAsync(user).Wait();
            return Ok();
        }

        // POST: api/SoftITOFlixUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<string> PostSoftITOFlixUser(SoftITOFlixUser softITOFlixUser)
        {
            if(User.Identity!.IsAuthenticated==true)
            {
                return BadRequest();
            }
            IdentityResult identityResult = _signInManager.UserManager.CreateAsync(softITOFlixUser, softITOFlixUser.Password).Result;

            if (identityResult != IdentityResult.Success)
            {
                return identityResult.Errors.FirstOrDefault()!.Description;
            }
            return Ok();
        }

        // DELETE: api/SoftITOFlixUsers/5
        [HttpDelete("{id}")]
        public ActionResult DeleteSoftITOFlixUser(long id)
        {
            SoftITOFlixUser? user = null;

            if (User.IsInRole("CustomerRepresentative") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
                {
                    return Unauthorized();
                }
            }
            user = _signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }
            user.Passive = true;
            _signInManager.UserManager.UpdateAsync(user).Wait();
            return Ok();
        }

        [HttpPost("LogIn")]
        public bool LogIn(LogInModel logInModel)
        {
            Microsoft.AspNetCore.Identity.SignInResult signInResult;
            SoftITOFlixUser applicationUser = _signInManager.UserManager.FindByNameAsync(logInModel.UserName).Result;

            if (applicationUser == null)
            {
                return false;
            }
            if(applicationUser.Passive == true)
            {
                return false;
            }
            signInResult = _signInManager.PasswordSignInAsync(applicationUser, logInModel.Password, false, false).Result;
            return signInResult.Succeeded;
        }
    }
}
