using System.ComponentModel.DataAnnotations;

namespace MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Motocikl
{
    public record AzuriranMotociklDTO
    {
        [Required]
        [MaxLength(30)]
        public string BrendModel { get; set; }

        [Required]
        [Range(1999, 2026, ErrorMessage = "Godina proizvodnje mora biti između 1999 i 2026.")]
        public int GodinaProizvodnje { get; set; }
    }
}
