using Application.Dtos.Device;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceService _deviceService;

        public DeviceController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// Obtém a lista de todos os dispositivos.
        /// </summary>
        /// <returns>Lista de dispositivos.</returns>
        /// <response code="200">Retorna a lista de dispositivos.</response>
        [HttpGet]
        public async Task<ActionResult<List<DeviceResponseDTO>>> GetDevices()
        {
            var devices = await _deviceService.GetDevicesAsync();
            return Ok(devices);
        }

        /// <summary>
        /// Obtém um dispositivo pelo seu identificador único (ID).
        /// </summary>
        /// <param name="id">Identificador único do dispositivo (UUID).</param>
        /// <returns>O dispositivo com o ID especificado.</returns>
        /// <response code="200">Retorna o dispositivo com o ID especificado.</response>
        /// <response code="404">Se o dispositivo não for encontrado.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceResponseDTO>> GetDeviceById(Guid id)
        {
            var device = await _deviceService.GetDeviceByIdAsync(id);
            return Ok(device);
        }

        /// <summary>
        /// Cria um novo dispositivo.
        /// </summary>
        /// <param name="deviceRequestDTO">Detalhes do dispositivo a ser criado.</param>
        /// <returns>O dispositivo criado com os detalhes.</returns>
        /// <response code="201">Dispositivo criado com sucesso.</response>
        [HttpPost]
        public async Task<ActionResult<DeviceResponseDTO>> CreateDevice([FromBody] DeviceRequestDTO deviceRequestDTO)
        {
            var createdDevice = await _deviceService.CreateDeviceAsync(deviceRequestDTO);
            return CreatedAtAction(nameof(GetDeviceById), new { id = createdDevice.Id }, createdDevice);
        }

        /// <summary>
        /// Atualiza as informações de um dispositivo existente.
        /// </summary>
        /// <param name="id">Identificador único do dispositivo (UUID).</param>
        /// <param name="deviceRequestDTO">Dados atualizados do dispositivo.</param>
        /// <returns>O dispositivo atualizado.</returns>
        /// <response code="200">Retorna o dispositivo atualizado.</response>
        /// <response code="404">Se o dispositivo não for encontrado.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<DeviceResponseDTO>> UpdateDevice(Guid id, [FromBody] DeviceRequestDTO deviceRequestDTO)
        {
            var updatedDevice = await _deviceService.UpdateDeviceAsync(id, deviceRequestDTO);
            return Ok(updatedDevice);
        }

        /// <summary>
        /// Deleta um dispositivo pelo seu identificador único (ID).
        /// </summary>
        /// <param name="id">Identificador único do dispositivo (UUID).</param>
        /// <returns>Resultado da exclusão.</returns>
        /// <response code="204">Dispositivo deletado com sucesso.</response>
        /// <response code="404">Se o dispositivo não for encontrado.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            await _deviceService.DeleteDeviceAsync(id);
            return NoContent();
        }
    }
}
