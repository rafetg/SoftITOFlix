using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftITOFlix.Models
{
    public class Restriction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; } = "";
    }
}
