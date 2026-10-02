
using System.ComponentModel.DataAnnotations;

namespace MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik
{
    public record RegistracijaDTO
    {
        [Required]
        [MaxLength(30)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(30)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string JMBG { get; set; }

        [Required]
        [MaxLength(30)]
        public string Imejl { get; set; }

        [Required]
        [MaxLength(50)]
        public string Adresa { get; set; }

        [Required]
        public long Telefon { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        public string BrVozackeDozvole { get; set; }

        [Required]
        public string Lozinka { get; set; }
    }
}
