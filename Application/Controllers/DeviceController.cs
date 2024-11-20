using Application.Dtos.Device;
using Application.Exceptions;
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

        // GET /api/devices
        // Retrieves a list of all devices.
        // Responses:
        //   200: Returns the list of devices.
        [HttpGet]
        public async Task<ActionResult<List<DeviceResponseDTO>>> GetDevices()
        {
            var devices = await _deviceService.GetDevicesAsync();
            return Ok(devices);
        }

        // GET /api/devices/{id}
        // Retrieves a device by its unique identifier (ID).
        // Parameters:
        //   id (Guid): The device's unique identifier (UUID).
        // Responses:
        //   200: Returns the device with the specified ID.
        //   404: If the device is not found.
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceResponseDTO>> GetDeviceById(Guid id)
        {
            try
            {
                var device = await _deviceService.GetDeviceByIdAsync(id);
                return Ok(device);
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Device not found" });
            }
        }

        // POST /api/devices
        // Creates a new device.
        // Request Body: DeviceRequestDTO (device details)
        // Responses:
        //   201: Device created successfully, returns the device details.
        [HttpPost]
        public async Task<ActionResult<DeviceResponseDTO>> CreateDevice([FromBody] DeviceRequestDTO deviceRequestDTO)
        {
            var createdDevice = await _deviceService.CreateDeviceAsync(deviceRequestDTO);
            return CreatedAtAction(nameof(GetDeviceById), new { id = createdDevice.Id }, createdDevice);
        }

        // PUT /api/devices/{id}
        // Updates an existing device's information.
        // Parameters:
        //   id (Guid): The device's unique identifier (UUID).
        //   deviceRequestDTO (DeviceRequestDTO): The updated device data.
        // Responses:
        //   200: Returns the updated device.
        //   404: If the device is not found.
        [HttpPut("{id}")]
        public async Task<ActionResult<DeviceResponseDTO>> UpdateDevice(Guid id, [FromBody] DeviceRequestDTO deviceRequestDTO)
        {
            try
            {
                var updatedDevice = await _deviceService.UpdateDeviceAsync(id, deviceRequestDTO);
                return Ok(updatedDevice);
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Device not found" });
            }
        }

        // DELETE /api/devices/{id}
        // Deletes a device by its unique identifier (ID).
        // Parameters:
        //   id (Guid): The device's unique identifier (UUID).
        // Responses:
        //   204: If the device was successfully deleted.
        //   404: If the device is not found.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            try
            {
                await _deviceService.DeleteDeviceAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Device not found" });
            }
        }
    }
}
