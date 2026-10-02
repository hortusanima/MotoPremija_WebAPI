
using System.ComponentModel.DataAnnotations;

namespace MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik
{
    public record PrijavaDTO
    {
        [Required]
        [MaxLength(30)]
        public string Imejl { get; set; }
        [Required]
        public string Lozinka { get; set; }
    }
}
