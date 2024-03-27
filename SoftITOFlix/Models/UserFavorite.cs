using System.ComponentModel.DataAnnotations.Schema;

namespace SoftITOFlix.Models
{
    public class UserFavorite
    {
        public long UserId { get; set; }
        public int MediaId { get; set; }
        [ForeignKey("UserId")]
        public SoftITOFlixUser? SoftITOFlixUser { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }
    }
}
