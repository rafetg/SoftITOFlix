using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftITOFlix.Models
{
    public class Category
    {
        public short Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50,MinimumLength = 2)]
        public string Name { get; set; } = "";
    }
}
