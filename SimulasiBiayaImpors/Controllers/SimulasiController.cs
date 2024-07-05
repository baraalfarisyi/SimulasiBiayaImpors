using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulasiBiayaImpor.SimulasiContext;
using SimulasiBiayaImpors.Models;
using System.Text.Json;

namespace SImulasiBiayaImpor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulasiController : ControllerBase
    {
        private readonly SimulasiContext _context;
        private readonly HttpClient _httpClient;

        public SimulasiController(SimulasiContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost]
        public async Task<IActionResult> PostSimulasi([FromBody] SimulasiRequest request)
        {
            var uraianResponse = await _httpClient.GetAsync($"https://api-hub.ilcs.co.id/my/n/barang?hs_code={request.KodeBarang}");
            if (!uraianResponse.IsSuccessStatusCode)
            {
                return BadRequest("Gagal mendapatkan uraian barang");
            }

            var uraianContent = await uraianResponse.Content.ReadAsStringAsync();
            var uraianJson = JsonDocument.Parse(uraianContent);
            var uraianBarang = uraianJson.RootElement.GetProperty("description").GetString();

            var tarifResponse = await _httpClient.GetAsync($"https://api-hub.ilcs.co.id/my/n/tarif?hs_code={request.KodeBarang}");
            if (!tarifResponse.IsSuccessStatusCode)
            {
                return BadRequest("Gagal mendapatkan tarif biaya impor");
            }

            var tarifContent = await tarifResponse.Content.ReadAsStringAsync();
            var tarifJson = JsonDocument.Parse(tarifContent);
            var bm = tarifJson.RootElement.GetProperty("bm").GetInt32();

            var nilaiBm = (request.NilaiKomoditas * bm) / 100;

            var simulasi = new BiayaImpor
            {
                Id = Guid.NewGuid(),
                KodeBarang = request.KodeBarang,
                UraianBarang = uraianBarang,
                Bm = bm,
                NilaiKomoditas = request.NilaiKomoditas,
                NilaiBm = nilaiBm,
                WaktuInsert = DateTime.UtcNow
            };

            _context.BiayaImpors.Add(simulasi);
            await _context.SaveChangesAsync();

            return Ok(simulasi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSimulasi(Guid id)
        {
            var simulasi = await _context.BiayaImpors.FindAsync(id);

            if (simulasi == null)
            {
                return NotFound();
            }

            return Ok(simulasi);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSimulasi()
        {
            var simulasiList = await _context.BiayaImpors.ToListAsync();
            return Ok(simulasiList);
        }
    }

    public class SimulasiRequest
    {
        public string KodeBarang { get; set; }
        public float NilaiKomoditas { get; set; }
    }
}

