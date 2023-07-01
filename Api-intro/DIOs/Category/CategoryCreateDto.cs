using System.ComponentModel.DataAnnotations;

namespace Api_intro.DIOs.Category
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
