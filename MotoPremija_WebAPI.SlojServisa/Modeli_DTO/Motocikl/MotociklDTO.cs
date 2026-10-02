
using MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Motocikl
{
    public record MotociklDTO
    {
        [Required]
        [MaxLength(30)]
        public string BrendModel { get; set; }

        [Required]
        [Range(1999, 2026, ErrorMessage = "Godina proizvodnje mora biti između 1999 i 2026.")]
        public int GodinaProizvodnje { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string BrSaobracajneDozvole { get; set; }

        [Required]
        [Column("primarnaupotreba")]
        public PrimarnaUpotreba PrimarnaUpotreba { get; set; }
    }
}
