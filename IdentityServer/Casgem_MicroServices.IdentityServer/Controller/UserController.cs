using Casgem_MicroServices.IdentityServer.DTOs;
using Casgem_MicroServices.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Casgem_MicroServices.IdentityServer.Controller
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
        {
            var user = new ApplicationUser()
            {
                City = signUpDTO.City,
                Email = signUpDTO.Mail,
                UserName = signUpDTO.UserName
            };
            var result= await _userManager.CreateAsync(user,signUpDTO.Password);
            if (result.Succeeded)
            {
                return Ok("Kullanıcı Kaydı Oluşturuldu");
            }
            else
            {
                return Ok("Bir Hata Oluştu");
            }
            
        }
    }
}
