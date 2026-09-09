using CNMSDataAPI.Models;
using CNMSDataAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNMSDataAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CNMSController : ControllerBase
    {
        private readonly ICNMSService _cnmsService;

        public CNMSController(ICNMSService cnmsService)
        {
            _cnmsService = cnmsService;
        }

        [HttpPost("GetData")]
        public async Task<IActionResult> GetData([FromBody] CNMSRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.BizSrc))
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Data = "BizSrc is required."
                });
            }

            try
            {
                var data = await _cnmsService.GetCNMSDataAsync(request.BizSrc);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse
                {
                    Success = false,
                    Data = ex.Message
                });
            }
        }
    }
}