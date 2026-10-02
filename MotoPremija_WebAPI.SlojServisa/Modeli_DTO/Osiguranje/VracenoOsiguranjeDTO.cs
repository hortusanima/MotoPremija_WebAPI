

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Osiguranje
{
    public record VracenoOsiguranjeDTO
    {
        [Required]
        [StringLength(9, MinimumLength = 9)]
        public string BrPolise { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Godišnja premija ne može biti negativna.")]
        public double GodisnjaPremija { get; set; }
    }
}
