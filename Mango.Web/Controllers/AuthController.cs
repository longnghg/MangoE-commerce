using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            loginRequestDto.UserName = "user5@gmail";
            loginRequestDto.Password = "123!@#Aa";
            return View(loginRequestDto);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            try
            {


                ResponseDto? responseDto = await _authService.LoginAsync(loginRequestDto);
                if (responseDto != null && responseDto.IsSuccess)
                {
                    LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
                    _tokenProvider.SetToken(loginResponseDto.Token);

                    await SignInUserAsync(loginResponseDto);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = responseDto.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error from server";

            }
            return View(loginRequestDto);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); 
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text = SD.RoleAdmin,Value = SD.RoleAdmin},
                new SelectListItem{Text = SD.RoleCustomer,Value = SD.RoleCustomer},

            };
            ViewBag.roleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {
            ResponseDto? result = await _authService.RegisterAsync(registrationRequestDto);
            ResponseDto? assignRole;

            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(registrationRequestDto.Role))
                {
                    registrationRequestDto.Role = SD.RoleCustomer;
                }

                assignRole = await _authService.AssignRoleAsync(registrationRequestDto);
                
                if(assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration successfull";
                    return RedirectToAction(nameof(Login));
                }
            }

            TempData["error"] = result.Message;

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text = SD.RoleAdmin,Value = SD.RoleAdmin},
                new SelectListItem{Text = SD.RoleCustomer,Value = SD.RoleCustomer},

            };
            ViewBag.roleList = roleList;
            return View(registrationRequestDto);
        }

        private async Task SignInUserAsync(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);
            ClaimsPrincipal principal = RegisterPrincipal(jwt);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        private static ClaimsPrincipal RegisterPrincipal(JwtSecurityToken jwt)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));


            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));


            //IEnumerable<Claim> roleClaimList = jwt.Claims.Where(u => u.Type == "role");
            identity.AddClaims(jwt.Claims.Where(u => u.Type == "role"));

            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}
