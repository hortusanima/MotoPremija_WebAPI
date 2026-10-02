using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni;

namespace MotoPremija_WebAPI.SlojPodataka.Modeli
{
    [Table("korisnik")]
    public class Korisnik
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("ime")]
        public string Ime { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("prezime")]
        public string Prezime { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        [Column("jmbg")]
        public string JMBG { get; set; }

        [Required]
        [MaxLength(30)]
        [Column("imejl")]
        public string Imejl { get; set; }

        [Required]
        [Column("uloga")]
        public UlogaKorisnika Uloga { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("adresa")]
        public string Adresa { get; set; }

        [Required]
        [Column("telefon")]
        public long Telefon { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        [Column("brvozackedozvole")]
        public string BrVozackeDozvole { get; set; }

        [Required]
        [MaxLength(512)]
        [Column("lozinkahash")]
        public string LozinkaHash { get; set; }

        [Required]
        [MaxLength(512)]
        [Column("lozinkasalt")]
        public string LozinkaSalt { get; set; }
    }
}
