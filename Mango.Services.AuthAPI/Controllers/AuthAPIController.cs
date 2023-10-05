using Azure;
using Mango.Services.AuthAPI.Compositions;
using Mango.Services.AuthAPI.Models.Dtos;
using Mango.Services.AuthAPI.Service.IService;
using Mango.Services.CouponAPI.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly Func<ArchiveKind, IArchivingProcess> _archivingProcess;
        private readonly IAuthService _authService;

        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService, Func<ArchiveKind, IArchivingProcess> archivingProcess)
        {
            _authService = authService;
            _response = new();
            _archivingProcess = archivingProcess;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost]
        [Route("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            IArchivingContext archivingContext = new ArchivingContext("longGeo", "LongContainer");


            await _archivingProcess(ArchiveKind.EngagementArchive).ExecuteAsync(archivingContext);




            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }
    }
}
