using System.ComponentModel.DataAnnotations;

namespace SimulasiBiayaImpors.Models
{
    public class BiayaImpor
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(8)]
        public string KodeBarang { get; set; }
        [StringLength(200)]
        public string UraianBarang { get; set; }
        public int Bm { get; set; }
        public float NilaiKomoditas { get; set; }
        public float NilaiBm { get; set; }
        public DateTime WaktuInsert { get; set; }
    }
}
