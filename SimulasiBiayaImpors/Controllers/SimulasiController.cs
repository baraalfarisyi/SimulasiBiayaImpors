using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            var url = $"https://api-hub.ilcs.co.id/my/n/barang?hs_code={request.KodeBarang}";
            var responseBody = await _httpClient.GetStringAsync(url);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

            string uraianBarang = null;
            int bm = 0;

            if (apiResponse != null && apiResponse.data != null && apiResponse.data.Count > 0)
            {
                var firstItem = apiResponse.data.First();
                uraianBarang = firstItem.sub_header;
                bm = firstItem.bm;

                // Mengeluarkan uraianBarang dan bm
                Console.WriteLine($"Uraian Barang: {uraianBarang}");
                Console.WriteLine($"BM: {bm}");
            }
            else
            {
                Console.WriteLine("No data found in the response.");
            }

            var nilaiBm = (request.NilaiKomoditas * bm) / 100;

            var biayaImpor = new BiayaImpor
            {
                Id = Guid.NewGuid(),
                KodeBarang = request.KodeBarang,
                UraianBarang = uraianBarang,
                Bm = bm,
                NilaiKomoditas = request.NilaiKomoditas,
                NilaiBm = nilaiBm,
                WaktuInsert = DateTime.UtcNow
            };

            // Anda bisa menggunakan objek biayaImpor sesuai kebutuhan Anda


            _context.BiayaImpors.Add(biayaImpor);
            await _context.SaveChangesAsync();

            return Ok(biayaImpor);
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

    public class DataItem
    {
        public string hs_code { get; set; }
        public int bm { get; set; }
        public string ppnbm { get; set; }
        public string cukai { get; set; }
        public string bk { get; set; }
        public string ppnbk { get; set; }
        public string uraian_id { get; set; }
        public string sub_header { get; set; }
    }

    public class ApiResponse
    {
        public List<DataItem> data { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }

}

