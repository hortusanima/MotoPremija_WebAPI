using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotoPremija_WebAPI.SlojPodataka.Modeli
{
    [Table("tiposiguranja")]
    public class TipOsiguranja
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("nazivosiguranja")]
        public string NazivOsiguranja { get; set; }

        [Required]
        [Column("baznapremija")]
        [Range(0, double.MaxValue, ErrorMessage = "Bazna premija ne može biti negativna.")]
        public double BaznaPremija { get; set; }
    }
}
