using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Movie
    {
        [Key]
        public int ImdbID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Title { get; set; } = "";
        public int Year { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Type { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Poster { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Plot { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Rated { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Released { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Runtime { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Genre { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Director { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Writer { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Actors { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Language { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Country { get; set; } = "";
        public double Rating { get; set;}
    }
}
