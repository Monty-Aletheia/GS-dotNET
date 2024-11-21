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

        /// <summary>
        /// Obtém todos os dispositivos associados a um usuário.
        /// </summary>
        /// <param name="userId">O ID único do usuário.</param>
        /// <returns>Lista de dispositivos do usuário.</returns>
        /// <response code="200">Retorna a lista de dispositivos do usuário.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllUserDevicesAsync(Guid userId)
        {
            var userDevices = await _userDeviceService.GetAllUserDevicesAsync(userId);
            return Ok(userDevices);
        }

        /// <summary>
        /// Obtém um dispositivo específico associado a um usuário.
        /// </summary>
        /// <param name="userId">O ID único do usuário.</param>
        /// <param name="userDeviceId">O ID único do dispositivo do usuário.</param>
        /// <returns>O dispositivo do usuário especificado.</returns>
        /// <response code="200">Retorna o dispositivo do usuário.</response>
        /// <response code="404">Se o dispositivo não for encontrado.</response>
        [HttpGet("{userDeviceId}")]
        public async Task<IActionResult> GetUserDeviceAsync(Guid userId, Guid userDeviceId)
        {
            var userDevice = await _userDeviceService.GetUserDeviceAsync(userId, userDeviceId);
            return Ok(userDevice);
        }

        /// <summary>
        /// Adiciona um dispositivo ao usuário.
        /// </summary>
        /// <param name="userId">O ID único do usuário.</param>
        /// <param name="userDeviceDto">Os dados do dispositivo a ser adicionado.</param>
        /// <returns>O dispositivo adicionado.</returns>
        /// <response code="201">Dispositivo adicionado com sucesso.</response>
        [HttpPost]
        public async Task<IActionResult> AddDeviceToUserAsync(Guid userId, [FromBody] UserDeviceDTO userDeviceDto)
        {
            var userDeviceResponse = await _userDeviceService.AddDeviceToUserAsync(userId, userDeviceDto);
            return Created("", userDeviceResponse);
        }

        /// <summary>
        /// Atualiza as informações de um dispositivo associado ao usuário.
        /// </summary>
        /// <param name="userId">O ID único do usuário.</param>
        /// <param name="userDeviceId">O ID único do dispositivo.</param>
        /// <param name="estimatedUsageHours">O número estimado de horas de uso do dispositivo.</param>
        /// <returns>O dispositivo atualizado.</returns>
        /// <response code="200">Retorna o dispositivo atualizado.</response>
        /// <response code="404">Se o dispositivo não for encontrado.</response>
        [HttpPut("{userDeviceId}")]
        public async Task<IActionResult> UpdateUserDeviceAsync(Guid userId, Guid userDeviceId, [FromBody] double estimatedUsageHours)
        {
            var userDeviceResponse = await _userDeviceService.UpdateUserDeviceAsync(userDeviceId, userId, estimatedUsageHours);
            return Ok(userDeviceResponse);
        }

        /// <summary>
        /// Remove um dispositivo do usuário.
        /// </summary>
        /// <param name="userId">O ID único do usuário.</param>
        /// <param name="userDeviceId">O ID único do dispositivo a ser removido.</param>
        /// <returns>Resultado da remoção do dispositivo.</returns>
        /// <response code="204">Dispositivo removido com sucesso.</response>
        /// <response code="404">Se o dispositivo não for encontrado.</response>
        [HttpDelete("{userDeviceId}")]
        public async Task<IActionResult> RemoveDeviceFromUserAsync(Guid userId, Guid userDeviceId)
        {
            await _userDeviceService.RemoveDeviceFromUserAsync(userId, userDeviceId);
            return NoContent();
        }
    }
}
