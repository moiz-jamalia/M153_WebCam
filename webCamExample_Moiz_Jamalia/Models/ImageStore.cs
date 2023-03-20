using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webCamExample_Moiz_Jamalia.Models
{
    [Table("ImageStore")]
    public class ImageStore
    {
        [Key]
        public int ImageID { get; set; }

        public string? ImageBase64String { get; set; }
     
        public DateTime? CreateDate { get; set; }
    }
}
