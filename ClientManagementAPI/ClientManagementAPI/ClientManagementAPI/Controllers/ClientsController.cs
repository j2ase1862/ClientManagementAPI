using Microsoft.AspNetCore.Mvc;
using ClientManagementAPI.Models;
using ClientManagementAPI.Services;

namespace ClientManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IniFileService _iniFileService;

        public ClientsController()
        {
            // INI 파일 경로 설정 (프로젝트 루트 디렉토리 내 Config.ini)
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Config.ini");
            _iniFileService = new IniFileService(filePath);
        }

        [HttpPost]
        public IActionResult SaveClient([FromBody] ClientData clientData)
        {
            try
            {
                if (clientData == null)
                {
                    return BadRequest(new { message = "Invalid client data." });
                }

                // INI 파일에 데이터 저장
                _iniFileService.SaveToIniFile(clientData);

                return Ok(new { message = "Client data saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}