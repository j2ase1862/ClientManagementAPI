using Microsoft.AspNetCore.Mvc;
using ClientManagementAPI.Models;
using ClientManagementAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ClientManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveClient([FromBody] ClientData clientData)
        {
            try
            {
                if (clientData == null)
                {
                    return BadRequest(new { message = "Invalid client data." });
                }

                // 데이터베이스에 저장
                _context.Clients.Add(clientData);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Client data saved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // GET: api/clients
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                // 데이터베이스에서 모든 클라이언트 정보 조회
                var clients = await _context.Clients.ToListAsync();
                return Ok(clients); // JSON 형식으로 클라이언트 정보 반환
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}