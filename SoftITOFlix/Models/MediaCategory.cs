using System.ComponentModel.DataAnnotations.Schema;

namespace SoftITOFlix.Models
{
    public class MediaCategory
    {
        public int MediaId { get; set; }
        public short CategoryId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
