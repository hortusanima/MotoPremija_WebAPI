using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni;

namespace MotoPremija_WebAPI.SlojPodataka.Modeli
{
    [Table("motocikl")]
    public class Motocikl
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("brendmodel")]
        public string BrendModel { get; set; }

        [Required]
        [Range(1999, 2026, ErrorMessage = "Godina proizvodnje mora biti između 1999 i 2026.")]
        [Column("godinaproizvodnje")]
        public int GodinaProizvodnje { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        [Column("brsaobracajnedozvole")]
        public string BrSaobracajneDozvole { get; set; }

        [Required]
        [Column("primarnaupotreba")]
        public PrimarnaUpotreba PrimarnaUpotreba { get; set; }

        [Required]
        [Column("korisnikid")]
        public Guid KorisnikId { get; set; }

        [ForeignKey("KorisnikId")]
        public Korisnik Korisnik { get; set; }
    }
}
