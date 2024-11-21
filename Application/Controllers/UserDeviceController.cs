using Application.Dtos.UserDevice;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/users/{userId}/devices")]
    [ApiController]
    public class UserDeviceController : ControllerBase
    {
        private readonly UserDeviceService _userDeviceService;

        public UserDeviceController(UserDeviceService userDeviceService)
        {
            _userDeviceService = userDeviceService;
        }

        // GET api/users/{userId}/devices
        [HttpGet]
        public async Task<IActionResult> GetAllUserDevicesAsync(Guid userId)
        {
            var userDevices = await _userDeviceService.GetAllUserDevicesAsync(userId);
            return Ok(userDevices);
        }

        // GET api/users/{userId}/devices/{userDeviceId}
        [HttpGet("{userDeviceId}")]
        public async Task<IActionResult> GetUserDeviceAsync(Guid userId, Guid userDeviceId)
        {
            var userDevice = await _userDeviceService.GetUserDeviceAsync(userId, userDeviceId);
            return Ok(userDevice);
        }

        // POST api/users/{userId}/devices
        [HttpPost]
        public async Task<IActionResult> AddDeviceToUserAsync(Guid userId, [FromBody] UserDeviceDTO userDeviceDto)
        {
            var userDeviceResponse = await _userDeviceService.AddDeviceToUserAsync(userId, userDeviceDto);
            return Created("", userDeviceResponse);
        }

        // PUT api/users/{userId}/devices/{userDeviceId}
        [HttpPut("{userDeviceId}")]
        public async Task<IActionResult> UpdateUserDeviceAsync(Guid userId, Guid userDeviceId, [FromBody] double estimatedUsageHours)
        {
            var userDeviceResponse = await _userDeviceService.UpdateUserDeviceAsync(userDeviceId, userId, estimatedUsageHours);
            return Ok(userDeviceResponse);
        }

        // DELETE api/users/{userId}/devices/{userDeviceId}
        [HttpDelete("{userDeviceId}")]
        public async Task<IActionResult> RemoveDeviceFromUserAsync(Guid userId, Guid userDeviceId)
        {
            await _userDeviceService.RemoveDeviceFromUserAsync(userId, userDeviceId);
            return NoContent();
        }
    }
}
