using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dtos;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using System;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public AuthService(AppDbContext db,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;

        }
        static bool CompareStrings (string str1, string str2)
        {
            return string.Equals(str1,str2,StringComparison.OrdinalIgnoreCase);
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {

            var user = _db.ApplicationUsers.FirstOrDefault(
              delegate (ApplicationUser user) {
                  // Logic kiểm tra
                  if (string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
                  {
                      return true;
                  }
                  else
                  {
                      return false;
                  }
              }
             );

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // create role ì it does  not exists

                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user,roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                var user = _db.ApplicationUsers.FirstOrDefault(
                  delegate (ApplicationUser user) {
                      // Logic kiểm tra
                      if (string.Equals(user.UserName, loginRequestDto.UserName, StringComparison.OrdinalIgnoreCase))
                      {
                          return true;
                      }
                      else
                      {
                          return false;
                      }
                  }
                 );

                bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (user == null || !isValid)
                {
                    return new LoginResponseDto { User = null, Token = "" };
                }

                // If user was found, generate token here
                var roles= await _userManager.GetRolesAsync(user);

                var token = _jwtTokenGenerator.GenerateToken(user,roles);
                UserDto userDto = new()
                {
                    Email = user.Email,
                    ID = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber
                };

                LoginResponseDto loginResponseDto = new LoginResponseDto
                {
                    User = userDto,
                    Token = token
                };

                return loginResponseDto;
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u=> u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                        ID = userToReturn.Id
                    };
                    return string.Empty;
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
