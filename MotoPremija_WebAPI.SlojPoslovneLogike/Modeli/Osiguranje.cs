
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotoPremija_WebAPI.SlojPodataka.Modeli
{
    [Table("osiguranje")]
    public class Osiguranje
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        [Column("brpolise")]
        public string BrPolise { get; set; }

        [Required]
        [Column("godisnjapremija", TypeName = "decimal(10, 2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Godišnja premija ne može biti negativna.")]
        public double GodisnjaPremija { get; set; }

        [Required]
        [Column("motociklid")]
        public Guid MotociklId { get; set; }

        [ForeignKey("MotociklId")]
        public Motocikl Motocikl { get; set; }

        [Required]
        [Column("tiposiguranjaid")]
        public Guid TipOsiguranjaId { get; set; }

        [ForeignKey("TipOsiguranjaId")]
        public TipOsiguranja TipOsiguranja { get; set; }
    }
}
