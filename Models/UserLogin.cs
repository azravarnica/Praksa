using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Models
{
    public class UserLogin
    {
        [Key] public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Username { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Password { get; set; } = "";
    }
}
