using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        [DisplayName("Category Name")]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
